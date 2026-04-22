namespace InvNexus.NotificationService.Application.Events;

public interface INotificationEventHandler
{
    Task HandleGoodsReceivedAsync(GoodsReceivedEvent integrationEvent, CancellationToken cancellationToken);
    Task HandleSalesCompletedAsync(SalesCompletedEvent integrationEvent, CancellationToken cancellationToken);
}
