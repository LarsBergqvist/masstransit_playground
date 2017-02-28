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

            var consumer = new MessageConsumer();

            sbc.ReceiveEndpoint(host, "mymessagequeue", endpoint =>
            {
               endpoint.Handler<Message>(new MessageHandler<Message>(consumer.Consume));
            });

         });

         bus.Start();
         System.Console.WriteLine("Waiting for messages...");

         System.Console.ReadLine();
      }
   }
}
