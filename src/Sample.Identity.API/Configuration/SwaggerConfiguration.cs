using Microsoft.OpenApi.Models;

namespace Sample.Identity.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sample.Identity.API",
                    Description = "Web api to provide authentication services",
                    TermsOfService = new Uri("https://neogrid.com/br/termo-de-uso"),
                });
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Identity.API v1"));
        }
    }
}