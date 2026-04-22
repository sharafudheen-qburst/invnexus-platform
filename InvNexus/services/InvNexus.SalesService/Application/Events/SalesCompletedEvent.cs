namespace InvNexus.SalesService.Application.Events;

public record SalesCompletedEvent(Guid SalesOrderId, string SalesNumber, DateTime CompletedAtUtc);
