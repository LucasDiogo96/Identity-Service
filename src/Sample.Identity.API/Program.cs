using Sample.Identity.API.Configuration;
using Sample.Identity.API.Ioc;
using Sample.Identity.API.Midlewares;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add Application dependencies
builder.Services.AddApplicationDependencies(builder.Configuration);

builder.Services.AddValidatorConfiguration();

builder.Services.AddBusConfiguration(builder.Configuration);

// Add API configurations
builder.Services.AddCORSConfiguration();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();