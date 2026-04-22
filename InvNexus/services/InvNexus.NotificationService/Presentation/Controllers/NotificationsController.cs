using InvNexus.NotificationService.Application.DTOs;
using InvNexus.NotificationService.Application.Mediator;
using InvNexus.NotificationService.Application.Queries.GetNotificationById;
using InvNexus.NotificationService.Application.Queries.GetNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvNexus.NotificationService.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationsController(IQueryMediator queryMediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NotificationResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var notifications = await queryMediator.SendAsync(new GetNotificationsQuery(), cancellationToken);
        return Ok(notifications);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificationResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var notification = await queryMediator.SendAsync(new GetNotificationByIdQuery(id), cancellationToken);
        return notification is null ? NotFound() : Ok(notification);
    }
}
