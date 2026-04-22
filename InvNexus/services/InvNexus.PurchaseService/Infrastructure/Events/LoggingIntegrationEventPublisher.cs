using InvNexus.PurchaseService.Application.Events;

namespace InvNexus.PurchaseService.Infrastructure.Events;

public class LoggingIntegrationEventPublisher(ILogger<LoggingIntegrationEventPublisher> logger) : IIntegrationEventPublisher
{
    public Task PublishGoodsReceivedAsync(GoodsReceivedEvent integrationEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Published GoodsReceived event for PurchaseOrderId {PurchaseOrderId}, PurchaseNumber {PurchaseNumber} at {ReceivedAtUtc}",
            integrationEvent.PurchaseOrderId,
            integrationEvent.PurchaseNumber,
            integrationEvent.ReceivedAtUtc);

        return Task.CompletedTask;
    }
}
