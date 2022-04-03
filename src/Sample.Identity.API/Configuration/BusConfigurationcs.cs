using MassTransit;

namespace Sample.Identity.API.Configuration
{
    public static class BusConfiguration
    {
        public static void AddBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration.GetConnectionString("RabbitMQ")), "/");

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}