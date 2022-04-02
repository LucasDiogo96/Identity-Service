using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Services.Zenvia.Models;

namespace Sample.Identity.Infra.Services.Zenvia
{
    public class ZenviaService : ISmsService
    {
        private readonly HttpClient client;
        private readonly ZenviaSettings settings;
        private readonly ILogger<ZenviaService> logger;

        public ZenviaService(IOptions<ZenviaSettings> settings, IHttpClientFactory client, ILogger<ZenviaService> logger)
        {
            this.logger = logger;

            this.settings = settings.Value;

            this.client = client.CreateClient();

            // Set request headers
            this.client.DefaultRequestHeaders.Add("Accept", "application/json");
            this.client.DefaultRequestHeaders.Add("X-API-Token", this.settings.SecretKey);
            ;
        }

        public async Task SendAsync(string phone, string message)
        {
            try
            {
                // Create payload
                ZenviaSmsBody payload = new ZenviaSmsBody(settings.From, phone, message);

                // Create http content
                StringContent content = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }), Encoding.UTF8, "application/json");

                logger.LogInformation($"Sending sms to {phone} with the following message: {message}.");

                // Send
                HttpResponseMessage response = await client.PostAsync(settings.Uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    logger.LogWarning($"An error was occured during the sms send: ", json);
                }

                logger.LogInformation($"An sms was sent. | Phone : {phone}.");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error was occured during the sms send to {phone} with the following message {message}.");
            }
        }
    }
}