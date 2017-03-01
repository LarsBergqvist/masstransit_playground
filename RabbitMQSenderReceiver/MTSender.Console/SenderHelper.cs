using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MTCommon;

namespace MTSender.Console
{
   public class SenderHelper
   {
      private string _address;

      public SenderHelper(string address)
      {
         _address = address;
      }

      public async Task SendUser(IBusControl busControl, AddUser addUser, Action<string> callback)
      {
         var sendendpoint = busControl.GetSendEndpoint(new Uri(new Uri(_address), "add_user_item_queue")).Result;

         await sendendpoint.Send(addUser);
      }

      public async Task SendOrderItem(IBusControl busControl, AddOrderItem addUOrderItem, Action<string> callback)
      {
         var sendendpoint = busControl.GetSendEndpoint(new Uri(new Uri(_address), "add_order_item_queue")).Result;

         await sendendpoint.Send(addUOrderItem);
      }

   }
}
