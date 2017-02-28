using System;
using MassTransit;
using MTCommon;

namespace MTSender.Console
{
   class Program
   {
      static void Main(string[] args)
      {
         var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
         {
            var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
            {
               h.Username("guest");
               h.Password("guest");
            });
         });

         bus.Start();

         while (true)
         {
            System.Console.WriteLine("Enter a message:");
            var mess = System.Console.ReadLine();
            bus.Publish(new Message { Value = mess });
         }

      }
   }
}
