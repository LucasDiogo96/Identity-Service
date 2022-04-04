using Microsoft.EntityFrameworkCore;
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
using Sample.Identity.Infra.Services.Sendgrid;
using Sample.Identity.Infra.Services.Sendgrid.Models;
using Sample.Identity.Infra.Services.Zenvia;
using Sample.Identity.Infra.Services.Zenvia.Models;
using SendGrid.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Sample.Identity.API.Ioc
{
    public static class DIConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add generic app dependencies
            services.AddMvc(options => options.Filters.Add<NotificationFilter>());
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<SendgridSettings>(configuration.GetSection("SendgridSettings"));
            services.Configure<ZenviaSettings>(configuration.GetSection("ZenviaSettings"));

            // Add notifications dependencies
            services.AddSendGrid(options =>
            {
                options.ApiKey = configuration.GetSection("SendgridSettings:SecretKey").Value;
            });
            services.AddScoped<ISmsService, ZenviaService>();
            services.AddScoped<IEmailService, SendGridService>();
            services.AddScoped<INotification, NotificationContext>();

            // Add services dependencies
            services.AddScoped<IUserDomainService, UserDomainService>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IRecoveryService, RecoveryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INotificationService, NotificationService>();

            // Add data access dependencies
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisCacheDB"));
            services.AddScoped(s => redis.GetDatabase());

            MongoContext context = new MongoContext(configuration);
            context.Configure();

            services.AddScoped<ICacheManager, RedisDBContext>();

            // Add providers
            services.AddTransient<IIdentityProvider, IdentityProvider>();
        }
    }
}