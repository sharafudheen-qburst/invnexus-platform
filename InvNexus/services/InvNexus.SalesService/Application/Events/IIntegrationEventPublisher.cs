namespace InvNexus.SalesService.Application.Events;

public interface IIntegrationEventPublisher
{
    Task PublishSalesCompletedAsync(SalesCompletedEvent integrationEvent, CancellationToken cancellationToken);
}
