namespace InvNexus.NotificationService.Application.Events;

public record SalesCompletedEvent(Guid SalesOrderId, string SalesNumber, DateTime CompletedAtUtc);
