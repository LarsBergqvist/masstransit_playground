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

         //
         // Publish two different messages based on user input
         //
         while (true)
         {
            System.Console.WriteLine("Enter a user:");
            var name = System.Console.ReadLine();
            bus.Publish(new AddUser() { Name = name });

            System.Console.WriteLine("Enter an order item name:");
            var orderitemname = System.Console.ReadLine();
            double price = 0.0;
            bool validPrice = false;
            while (!validPrice)
            {
               System.Console.WriteLine("Enter the price for the order item:");
               if (!double.TryParse(System.Console.ReadLine(), out price))
               {
                  System.Console.WriteLine("Not a valid price");
               }
               else
               {
                  validPrice = true;
               }
            }
            bus.Publish(new AddOrderItem() { Name = orderitemname, Price = price });
         }

      }
   }
}
