using CleanArchitecture.Api.Infrastructure;

namespace CleanArchitecture.Api;
internal static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
