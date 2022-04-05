using Sample.Identity.API.Configuration;
using Sample.Identity.API.Ioc;
using Sample.Identity.API.Midlewares;
using Sample.Identity.Infra.Providers;
using Serilog;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Serilog.ILogger logger = LoggerProvider.AddSerilog(builder.Configuration, env);

builder.Host.UseSerilog(logger);

// Add Application dependencies
builder.Services.AddApplicationDependencies(builder.Configuration);

builder.Services.AddValidatorConfiguration();

builder.Services.AddBusConfiguration(builder.Configuration);

// Add API configurations
builder.Services.AddCORSConfiguration();

builder.Services.AddAuthorizationSettings(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddHealthChecks();

// HTTP client factory
builder.Services.AddHttpClient();

// Builders
WebApplication? app = builder.Build();

app.UseCORSConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseMiddleware<ExceptionMiddleware>();

app.ConfigureValidatorLocation(app.Configuration);

app.UseHttpsRedirection();

app.UseAuthorizationSettings();

app.MapControllers();

app.Run();