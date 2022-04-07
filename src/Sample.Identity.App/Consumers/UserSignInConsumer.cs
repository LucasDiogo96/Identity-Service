using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Identity.Domain.Events;

namespace Sample.Identity.App.Consumers
{
    public class UserSignInConsumer : IConsumer<UserSignInEvent>
    {
        private readonly ILogger<UserSignInEvent> logger;

        public UserSignInConsumer(ILogger<UserSignInEvent> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UserSignInEvent> context)
        {
            UserSignInEvent message = context.Message;

            logger.LogInformation($"Processing.. | MessageId: {context.MessageId}. | Message Type: {typeof(UserSignInEvent)}.");

            // DO something like audit

            logger.LogInformation($"Completed.. | MessageId: {context.MessageId}. | Message Type: {typeof(UserSignInEvent)}.");

            await Task.CompletedTask;
        }
    }
}