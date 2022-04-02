namespace Sample.Identity.API.Configuration
{
    public static class CORSConfiguration
    {
        private const string Policy = "Sample.Identity";

        public static void AddCORSConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Policy, policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .WithMethods("GET", "POST", "PATCH", "OPTIONS");
                });
            });
        }

        public static void UseCORSConfiguration(this IApplicationBuilder app) => app.UseCors(Policy);
    }
}