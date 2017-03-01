using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MTCommon;

namespace MTReceiver.Console
{
   public class AddUserConsumer : IConsumer<AddUser>
   {
      public async Task Consume(ConsumeContext<AddUser> context)
      {
         // Simulate that the operation takes some time
         Thread.Sleep(10000);
         await System.Console.Out.WriteLineAsync(string.Format("New user received: {0}", context.Message.Name));
      }
   }
}
