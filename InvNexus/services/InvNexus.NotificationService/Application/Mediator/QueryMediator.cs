using Microsoft.Extensions.DependencyInjection;

namespace InvNexus.NotificationService.Application.Mediator;

public class QueryMediator(IServiceProvider serviceProvider) : IQueryMediator
{
    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = serviceProvider.GetRequiredService(handlerType);

        return await ((dynamic)handler).HandleAsync((dynamic)query, cancellationToken);
    }
}
