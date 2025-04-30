using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Api.Users;
public  static class DependencyInjectionApplication
{
    public static IServiceCollection AddNotificationHandlers(this IServiceCollection services, Assembly assembly)
    {
        Type handlerInterfaceType = typeof(INotificationHandler<>);

        var handlers = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Implementation = t, Interface = i })
            .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == handlerInterfaceType)
            .ToList();

        foreach (var handler in handlers)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }
}
