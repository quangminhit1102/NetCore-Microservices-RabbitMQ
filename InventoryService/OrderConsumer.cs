using MassTransit;
using Model;
using Newtonsoft.Json;

namespace OrderService.Settings
{
    public class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(context.Message)).ConfigureAwait(false);
        }
    }
}