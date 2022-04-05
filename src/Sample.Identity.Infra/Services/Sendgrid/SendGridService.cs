using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Services.Sendgrid.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Sample.Identity.Infra.Services.Sendgrid
{
    public class SendGridService : IEmailService
    {
        private readonly SendgridSettings settings;
        private readonly ISendGridClient client;
        private readonly ILogger<SendGridService> logger;

        public SendGridService(ISendGridClient client, IOptions<SendgridSettings> settings, ILogger<SendGridService> logger)
        {
            this.client = client;
            this.logger = logger;
            this.settings = settings.Value;
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(settings.From, settings.Name),
                Subject = subject
            };

            msg.AddContent(MimeType.Html, message);

            msg.AddTo(new EmailAddress(email));

            try
            {
                await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error was occured during the email send to {email}.");
            }
        }
    }
}