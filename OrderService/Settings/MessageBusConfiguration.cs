using MassTransit;
using Model;

namespace OrderService.Settings
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, MessageBusSetting messageBusSetting)
        {
            ArgumentNullException.ThrowIfNull(messageBusSetting);
            if (!messageBusSetting.IsValid())
            {
                throw new ArgumentException($"Invalid {nameof(messageBusSetting)}");
            }

            _ = ushort.TryParse(messageBusSetting.Port, out var port);

            // Add MassTransit
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");

                    cfg.Host(messageBusSetting.Host, port, "/", h =>
                    {
                        h.Username(messageBusSetting.UserName);
                        h.Password(messageBusSetting.Password);
                    });


                    //Config Direct Exchange
                    cfg.Message<Order>(x => x.SetEntityName("order-direct-exchange"));
                    cfg.Send<Order>(x =>
                    {
                        // use customerType for the routing key
                        x.UseRoutingKeyFormatter(context => "direct-exchange");
                    });
                    cfg.Publish<Order>(x =>
                    {
                        x.ExchangeType = "direct";
                    });

                    //// Config fanout Exchange
                    //cfg.Message<Order>(x => x.SetEntityName("order-fanout-exchange"));
                    //cfg.Publish<Order>(x =>
                    //{
                    //    x.ExchangeType = "fanout";
                    //});

                    //// Config Topic Exchange
                    //cfg.Message<Order>(x => x.SetEntityName("order-topic-exchange"));
                    //cfg.Publish<Order>(x =>
                    //{
                    //    x.ExchangeType = "topic";
                    //});
                });
            });

            return services;
        }
    }
}
