using InvNexus.NotificationService.Application.DTOs;
using InvNexus.NotificationService.Application.Interfaces;
using InvNexus.NotificationService.Application.Mediator;

namespace InvNexus.NotificationService.Application.Queries.GetNotifications;

public class GetNotificationsQueryHandler(INotificationRepository notificationRepository)
    : IQueryHandler<GetNotificationsQuery, IReadOnlyList<NotificationResponseDto>>
{
    public async Task<IReadOnlyList<NotificationResponseDto>> HandleAsync(GetNotificationsQuery query, CancellationToken cancellationToken)
    {
        var notifications = await notificationRepository.GetAllAsync(cancellationToken);

        return notifications
            .Select(notification => new NotificationResponseDto
            {
                Id = notification.Id,
                Type = notification.Type,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt
            })
            .ToList();
    }
}
