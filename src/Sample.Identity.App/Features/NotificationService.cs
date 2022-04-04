﻿using System.Reflection;
using System.Resources;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Extensions;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.App.Features
{
    public class NotificationService : INotificationService
    {
        private readonly ISmsService smsService;
        private readonly IEmailService emailService;

        public NotificationService(ISmsService smsService, IEmailService emailService)
        {
            this.smsService = smsService;
            this.emailService = emailService;
        }

        public async Task SendRecoverySms(string code, string phone)
        {
            string template = ResourceExtension.Get("SmsRecoveryMessage");

            // Get the message to send from resources
            string message = string.Format(template, code);

            // Send sms async
            await smsService.SendAsync(phone, message);
        }

        public async Task SendRecoveryEmail(string code, string email, string name)
        {
            // Get email template
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                                      "Resources/Templates/PasswordRecovery.html");

            // Get email template
            string template = File.ReadAllText(path);

            // Replace keys
            template = template.Replace("[#Name]", name)
                               .Replace("[#RecoveryCode]", code);

            string message = ResourceExtension.Get("EmailRecoverySubject");

            //Send async
            await emailService.SendAsync(email, message, template);
        }

        private string GetResource(string name)
        {
            ResourceManager resources = new ResourceManager("Sample.Identity.App.Features.Resources", typeof(NotificationService).Assembly);

            return resources.GetString(name);
        }
    }
}