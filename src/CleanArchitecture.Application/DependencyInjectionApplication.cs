using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;
public static class DependencyInjectionApplication
{
    public static IServiceCollection AddAplication(this IServiceCollection servicios)
    {
        servicios.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjectionApplication).Assembly);
        });
        return servicios;
    }
}
