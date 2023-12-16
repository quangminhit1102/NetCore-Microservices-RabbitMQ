using MassTransit;

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
                x.AddConsumer<OrderConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    //cfg.Host("amqp://guest:guest@localhost:5672");

                    cfg.Host(messageBusSetting.Host, port, "/", h =>
                    {
                        // Please use port like Uint => Can not connect
                        h.Username(messageBusSetting.UserName);
                        h.Password(messageBusSetting.Password);
                    });

                    //// ReceiveEndpoint Direct Exchange
                    //cfg.ReceiveEndpoint("order-queue", e =>
                    //{
                    //    e.Bind("order-direct-exchange", x =>
                    //    {
                    //        x.RoutingKey = "direct-exchange";
                    //        x.ExchangeType = "direct";
                    //    });

                    //    e.ConfigureConsumer<OrderConsumer>(ctx);
                    //});
                    // //-------------------------------------------------------------------

                    //// ReceiveEndpoint fanout Exchange
                    //cfg.ReceiveEndpoint("order-queue", e =>
                    //{
                    //    e.Bind("order-fanout-exchange", x =>
                    //    {
                    //        x.ExchangeType = "fanout";
                    //    });

                    //    e.ConfigureConsumer<OrderConsumer>(ctx);
                    //});
                    // //-------------------------------------------------------------------

                    // ReceiveEndpoint fanout Exchange
                    cfg.ReceiveEndpoint("order-queue", e =>
                    {
                        e.Bind("order-topic-exchange", x =>
                        {
                            x.ExchangeType = "topic";
                            x.RoutingKey = "Order.*";
                        });

                        e.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                    //-------------------------------------------------------------------

                });
            });

            return services;
        }
    }
}
