using Microsoft.Extensions.DependencyInjection;

namespace InvNexus.SalesService.Application.Mediator;

public class CommandMediator(IServiceProvider serviceProvider) : ICommandMediator
{
    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handler = serviceProvider.GetRequiredService(handlerType);

        return await ((dynamic)handler).HandleAsync((dynamic)command, cancellationToken);
    }
}
