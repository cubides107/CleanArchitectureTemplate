using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Worker;
public static class DependencyInjectionApplicationWorker
{
    public static IServiceCollection AddApplicationWorker(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionApplicationWorker).Assembly);
        });

        return services;
    }
}
