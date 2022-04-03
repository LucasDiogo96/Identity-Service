using FluentValidation;
using FluentValidation.AspNetCore;
using Sample.Identity.App.Transfers;
using Sample.Identity.App.Transfers.Recovery;
using Sample.Identity.App.Validators;
using Sample.Identity.Domain.Commands;

namespace Sample.Identity.API.Configuration
{
    public static class ValidatorConfiguration
    {
        public static void AddValidatorConfiguration(this IServiceCollection services)
        {
            // Fluent validator
            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<PasswordRecoveryTransfer>, PasswordRecoveryValidator>();
            services.AddTransient<IValidator<PasswordRecoveryRequestTransfer>, PasswordRecoveryRequestValidator>();
            services.AddTransient<IValidator<PasswordRecoveryConfirmTransfer>, PasswordRecoveryConfirmValidator>();
            services.AddTransient<IValidator<IdentitySignInTransfer>, IdentitySignInValidator>();
            services.AddTransient<IValidator<IdentityRefreshTransfer>, IdentityRefreshValidator>();
            services.AddTransient<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
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