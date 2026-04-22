using InvNexus.SalesService.Application.Events;

using InvNexus.SalesService.Application.Events;

namespace InvNexus.SalesService.Infrastructure.Events;

public class LoggingIntegrationEventPublisher(ILogger<LoggingIntegrationEventPublisher> logger) : IIntegrationEventPublisher
{
    public Task PublishSalesCompletedAsync(SalesCompletedEvent integrationEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Published SalesCompleted event for SalesOrderId {SalesOrderId}, SalesNumber {SalesNumber} at {CompletedAtUtc}",
            integrationEvent.SalesOrderId,
            integrationEvent.SalesNumber,
            integrationEvent.CompletedAtUtc);

        return Task.CompletedTask;
    }
}
