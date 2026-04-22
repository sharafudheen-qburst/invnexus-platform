using InvNexus.NotificationService.Application.DTOs;
using InvNexus.NotificationService.Application.Interfaces;
using InvNexus.NotificationService.Application.Mediator;

namespace InvNexus.NotificationService.Application.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler(INotificationRepository notificationRepository)
    : IQueryHandler<GetNotificationByIdQuery, NotificationResponseDto?>
{
    public async Task<NotificationResponseDto?> HandleAsync(GetNotificationByIdQuery query, CancellationToken cancellationToken)
    {
        var notification = await notificationRepository.GetByIdAsync(query.Id, cancellationToken);
        if (notification is null)
        {
            return null;
        }

        return new NotificationResponseDto
        {
            Id = notification.Id,
            Type = notification.Type,
            Message = notification.Message,
            CreatedAt = notification.CreatedAt
        };
    }
}
