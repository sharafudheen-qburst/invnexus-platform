using InvNexus.NotificationService.Application.DTOs;
using InvNexus.NotificationService.Application.Mediator;

namespace InvNexus.NotificationService.Application.Queries.GetNotificationById;

public record GetNotificationByIdQuery(Guid Id) : IQuery<NotificationResponseDto?>;
