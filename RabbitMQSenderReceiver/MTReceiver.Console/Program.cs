using System;
using MassTransit;
using MTCommon;

namespace MTReceiver.Console
{
   class Program
   {
      static void Main(string[] args)
      {
         var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
         {
            var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
            {
               h.Username("guest");
               h.Password("guest");
            });

            //
            // Defines two consumers for two different messages
            //

            var orderConsumer = new AddOrderConsumer();
            sbc.ReceiveEndpoint(host, "add_order_item_queue", endpoint =>
            {
               endpoint.Handler<AddOrderItem>(new MessageHandler<AddOrderItem>(orderConsumer.Consume));
            });

            var userConsumer = new AddUserConsumer();
            sbc.ReceiveEndpoint(host, "add_user_item_queue", endpoint =>
            {
               endpoint.Handler<AddUser>(new MessageHandler<AddUser>(userConsumer.Consume));
            });

         });

         bus.Start();
         System.Console.WriteLine("Waiting for messages...");

         System.Console.ReadLine();
      }
   }
}
