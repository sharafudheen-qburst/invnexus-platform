using InvNexus.NotificationService.Application.Events;
using InvNexus.NotificationService.Application.Interfaces;
using InvNexus.NotificationService.Domain.Entities;

namespace InvNexus.NotificationService.Infrastructure.Events;

public class NotificationEventHandler(INotificationRepository notificationRepository) : INotificationEventHandler
{
    public async Task HandleGoodsReceivedAsync(GoodsReceivedEvent integrationEvent, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Type = "GoodsReceived",
            Message = $"Goods received for purchase {integrationEvent.PurchaseNumber}",
            CreatedAt = DateTime.UtcNow
        };

        await notificationRepository.AddAsync(notification, cancellationToken);
    }

    public async Task HandleSalesCompletedAsync(SalesCompletedEvent integrationEvent, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Type = "SalesCompleted",
            Message = $"Sales order {integrationEvent.SalesNumber} completed",
            CreatedAt = DateTime.UtcNow
        };

        await notificationRepository.AddAsync(notification, cancellationToken);
    }
}
