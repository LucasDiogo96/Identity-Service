using MassTransit;
using Sample.Identity.App.Consumers;
using Sample.Identity.Domain.Events;

namespace Sample.Identity.API.Configuration
{
    public static class BusConfiguration
    {
        public static void AddBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserAddedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Add message retry
                    cfg.UseMessageRetry(r =>
                    {
                        // Retry delays will be approximately 0 sec, 3 sec, 9 sec, 25 sec and the fixed 30 sec,
                        r.Exponential(3, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(3));

                        // We don't want to retry here, the message content is invalid
                        r.Ignore<InvalidCastException>();
                    });

                    cfg.Host(new Uri(configuration.GetConnectionString("RabbitMQ")), "/");

                    cfg.Message<UserAddedEvent>(x =>
                    {
                        x.SetEntityName(configuration.GetSection("MassTransit:UserAdded:Topic").Value);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}