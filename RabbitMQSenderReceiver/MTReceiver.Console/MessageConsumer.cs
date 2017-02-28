using System.Threading.Tasks;
using MassTransit;
using MTCommon;

namespace MTReceiver.Console
{
   public class MessageConsumer : IConsumer<Message>
   {
      public async Task Consume(ConsumeContext<Message> context)
      {
         await System.Console.Out.WriteLineAsync($"Received: {context.Message.Value}");
      }
   }
}
