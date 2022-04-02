using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Identity.API.Midlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpContextAccessor accessor;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor accessor, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            this.accessor = accessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error was ocurred during the process. Trace Identifier: {accessor.HttpContext.TraceIdentifier}.");

                await HandleExceptionMessageAsync(accessor.HttpContext).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context)
        {
            string response = JsonSerializer.Serialize(new ValidationProblemDetails()
            {
                Title = "An error was occurred.",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path,
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(response);
        }
    }
}