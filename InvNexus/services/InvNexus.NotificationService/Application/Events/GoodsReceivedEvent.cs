namespace InvNexus.NotificationService.Application.Events;

public record GoodsReceivedEvent(Guid PurchaseOrderId, string PurchaseNumber, DateTime ReceivedAtUtc);
