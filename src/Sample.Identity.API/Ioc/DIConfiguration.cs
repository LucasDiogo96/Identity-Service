using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Identity.API.Filters;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Services;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;
using Sample.Identity.Infra.Services.Zenvia;

namespace Sample.Identity.API.Ioc
{
    public static class DIConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add infra dependencies
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddScoped<ISmsService, ZenviaService>();

            // Add domain layer dependencies
            services.AddScoped<INotification, NotificationContext>();

            // Web dependencies
            services.AddMvc(options => options.Filters.Add<NotificationFilter>());

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}