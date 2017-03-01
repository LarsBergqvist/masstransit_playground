using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MTCommon;

namespace MTReceiver.Console
{
   public class AddOrderConsumer : IConsumer<AddOrderItem>
   {
      public async Task Consume(ConsumeContext<AddOrderItem> context)
      {
         // Simulate that the operation takes some time
         Thread.Sleep(10000);
         await System.Console.Out.WriteLineAsync(string.Format("Received order: {0}, price={1}",context.Message.Name,context.Message.Price));
      }
   }
}
