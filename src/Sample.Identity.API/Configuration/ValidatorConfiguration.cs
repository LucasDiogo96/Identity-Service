using FluentValidation.AspNetCore;

namespace Sample.Identity.API.Configuration
{
    public static class ValidatorConfiguration
    {
        public static void AddValidatorConfiguration(this IServiceCollection services)
        {
            // Fluent validator
            services.AddMvc().AddFluentValidation();
        }

        public static void ConfigureValidatorLocation(this IApplicationBuilder app, IConfiguration configuration)
        {
            // setup message language
            string[] cultures = configuration.GetSection("DefaultLanguage").Get<string[]>();

            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(cultures[0])
            .AddSupportedCultures(cultures)
            .AddSupportedUICultures(cultures);

            app.UseRequestLocalization(localizationOptions);
        }
    }
}