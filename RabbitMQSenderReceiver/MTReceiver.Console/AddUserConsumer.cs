using System.Threading.Tasks;
using MassTransit;
using MTCommon;

namespace MTReceiver.Console
{
   public class AddUserConsumer : IConsumer<AddUser>
   {
      public async Task Consume(ConsumeContext<AddUser> context)
      {
         await System.Console.Out.WriteLineAsync(string.Format("New user received: {0}", context.Message.Name));
      }
   }
}
