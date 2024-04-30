using IntegrationEvents;
using MassTransit;

namespace WarehouseService.BusHandlers
{
    public class ItemCreatedEventConsumer : IConsumer<ItemCreatedEvent>
    {
        private readonly ILogger<ItemCreatedEventConsumer> _logger;

        public ItemCreatedEventConsumer(ILogger<ItemCreatedEventConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ItemCreatedEvent> context)
        {
            // Perform the necessary actions upon receiving the message
            _logger.LogInformation($"Product created - ID: {context.Message.Id}, Name: {context.Message.Name}, Price: {context.Message.Description}");

            await Task.CompletedTask;
        }
    }
}
