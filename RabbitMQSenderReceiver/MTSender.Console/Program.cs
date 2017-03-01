using System;
using MassTransit;
using MTCommon;

namespace MTSender.Console
{
   class Program
   {
      static void Main(string[] args)
      {
         const string address = "rabbitmq://localhost";

         var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
         {
            var host = sbc.Host(new Uri(address), h =>
            {
               h.Username("guest");
               h.Password("guest");
            });
         });

         bus.Start();

         var senderHelper = new SenderHelper(address);

         //
         // Send two different messages based on user input
         //
         while (true)
         {
            System.Console.WriteLine("Enter a user:");
            var name = System.Console.ReadLine();
            var addUser = new AddUser() {Name = name};
            senderHelper.SendUser(bus, addUser,ResultCallback);

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
            senderHelper.SendOrderItem(bus, new AddOrderItem() {Name = orderitemname, Price = price}, ResultCallback);
         }

      }

      static void ResultCallback(string result)
      {
         System.Console.WriteLine("Result: {0}",result);
      }
   }
}
