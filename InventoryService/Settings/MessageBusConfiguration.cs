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

            // Add MassTransit
            services.AddMassTransit(x =>
            {   
                x.AddConsumer<OrderConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                    
                    cfg.ReceiveEndpoint("order-queue", e =>
                    {
                        e.Bind("order-direct-exchange", x =>
                        {
                            x.RoutingKey = "direct-exchange";
                            x.ExchangeType = "direct";
                        });

                        e.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
