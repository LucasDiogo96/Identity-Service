using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sample.Identity.Domain.Contracts;

namespace Sample.Identity.API.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotification notificationContext;

        public NotificationFilter(INotification notificationContext)
        {
            this.notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (notificationContext.HasNotifications())
            {
                int status = (int)HttpStatusCode.BadRequest;

                string? path = context.RouteData.Values["controller"]?.ToString();

                if (path.Equals("Identity", StringComparison.OrdinalIgnoreCase))
                {
                    status = (int)HttpStatusCode.Unauthorized;
                }

                context.HttpContext.Response.StatusCode = status;
                context.HttpContext.Response.ContentType = "application/json";

                string response = HandleMessages(status, context.HttpContext.Request.Path);

                await context.HttpContext.Response.WriteAsync(response);

                return;
            }

            await next();
        }

        private string HandleMessages(int statusCode, string path)
        {
            ModelStateDictionary dictionary = new ModelStateDictionary();

            foreach (Domain.Common.Notification? item in notificationContext.GetNotifications())
            {
                dictionary.AddModelError(item.Key, $"{item.Code} - {item.Message}");
            }

            ValidationProblemDetails response = new ValidationProblemDetails(dictionary)
            {
                Status = statusCode,
                Instance = path
            };

            return System.Text.Json.JsonSerializer.Serialize(response);
        }
    }
}