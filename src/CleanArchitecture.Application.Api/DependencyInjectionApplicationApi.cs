using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Api;
public static class DependencyInjectionApplicationApi
{
    public static IServiceCollection AddApplicationApi(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionApplicationApi).Assembly);
        });

        return services;
    }
}
