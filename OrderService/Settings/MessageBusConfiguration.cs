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
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                });
            });

            return services;
        }
    }
}
