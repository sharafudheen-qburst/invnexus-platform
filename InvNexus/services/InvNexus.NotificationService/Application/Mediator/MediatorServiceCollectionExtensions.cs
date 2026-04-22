using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InvNexus.NotificationService.Application.Mediator;

public static class MediatorServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<ICommandMediator, CommandMediator>();
        services.AddScoped<IQueryMediator, QueryMediator>();

        var targetAssemblies = assemblies.Length > 0
            ? assemblies
            : [Assembly.GetExecutingAssembly()];

        var implementationTypes = targetAssemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false });

        foreach (var implementationType in implementationTypes)
        {
            var serviceTypes = implementationType
                .GetInterfaces()
                .Where(@interface => @interface.IsGenericType)
                .Where(@interface =>
                {
                    var genericType = @interface.GetGenericTypeDefinition();
                    return genericType == typeof(ICommandHandler<,>) || genericType == typeof(IQueryHandler<,>);
                });

            foreach (var serviceType in serviceTypes)
            {
                services.AddScoped(serviceType, implementationType);
            }
        }

        return services;
    }
}
