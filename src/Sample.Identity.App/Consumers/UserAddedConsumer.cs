using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Identity.Domain.Events;

namespace Sample.Identity.App.Consumers
{
    public class UserAddedConsumer : IConsumer<UserAddedEvent>
    {
        private readonly ILogger<UserAddedConsumer> logger;

        public UserAddedConsumer(ILogger<UserAddedConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UserAddedEvent> context)
        {
            UserAddedEvent message = context.Message;

            logger.LogInformation($"Processing.. | MessageId: {context.MessageId}. | Message Type: {typeof(UserAddedEvent)}.");

            // DO something like send a email

            logger.LogInformation($"Completed.. | MessageId: {context.MessageId}. | Message Type: {typeof(UserAddedEvent)}.");

            await Task.CompletedTask;
        }
    }
}