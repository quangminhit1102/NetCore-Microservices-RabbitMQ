using MassTransit;
using Model;

namespace OrderService.Settings
{
    public class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name).ConfigureAwait(false);
        }
    }
}