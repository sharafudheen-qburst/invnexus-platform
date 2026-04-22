namespace InvNexus.PurchaseService.Application.Events;

public interface IIntegrationEventPublisher
{
    Task PublishGoodsReceivedAsync(GoodsReceivedEvent integrationEvent, CancellationToken cancellationToken);
}
