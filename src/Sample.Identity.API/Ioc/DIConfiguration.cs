using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Identity.API.Filters;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Features;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Services;
using Sample.Identity.Infra.Contexts;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;
using Sample.Identity.Infra.Persistence;
using Sample.Identity.Infra.Providers;
using Sample.Identity.Infra.Services.Zenvia;
using StackExchange.Redis;

namespace Sample.Identity.API.Ioc
{
    public static class DIConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //// Add db context
            //services.AddDbContext<PersistenceContext>(
            //options => options.UseMongo(configuration["Tradeforce:ConnectionStrings:CosmosDB"], configuration["DatabaseName"],
            //options =>
            //{
            //    options.RequestTimeout(TimeSpan.FromSeconds(30));
            //}));

            services.AddMvc(options => options.Filters.Add<NotificationFilter>());
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisCacheDB"));
            services.AddScoped(s => redis.GetDatabase());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheManager, RedisDBContext>();
            services.AddScoped<ISmsService, ZenviaService>();
            services.AddTransient<IIdentityProvider, IdentityProvider>();

            services.AddScoped<INotification, NotificationContext>();
            services.AddScoped<IUserDomainService, UserDomainService>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}