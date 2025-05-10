using CleanArchitecture.Api.Infrastructure;
using CleanArchitecture.Api.Middlewares;

namespace CleanArchitecture.Api;
internal static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddExceptionHandler<InvalidFormatExceptionMiddleware>();
        services.AddProblemDetails();

        return services;
    }
}
