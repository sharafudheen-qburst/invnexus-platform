using InvNexus.NotificationService.Application.DTOs;
using InvNexus.NotificationService.Application.Mediator;

namespace InvNexus.NotificationService.Application.Queries.GetNotifications;

public record GetNotificationsQuery : IQuery<IReadOnlyList<NotificationResponseDto>>;
