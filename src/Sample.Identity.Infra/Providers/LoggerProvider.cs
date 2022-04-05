using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Sample.Identity.Infra.Providers
{
    public static class LoggerProvider
    {
        public static ILogger AddSerilog(IConfiguration configuration, string env)
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"sample-identity-{env?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                    })
                .Enrich.WithProperty("Environment", env)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}