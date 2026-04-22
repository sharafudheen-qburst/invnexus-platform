namespace InvNexus.SalesService.Application.Mediator;

public interface ICommandMediator
{
    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
}
