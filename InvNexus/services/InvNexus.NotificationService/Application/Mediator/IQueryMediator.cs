namespace InvNexus.NotificationService.Application.Mediator;

public interface IQueryMediator
{
    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
}
