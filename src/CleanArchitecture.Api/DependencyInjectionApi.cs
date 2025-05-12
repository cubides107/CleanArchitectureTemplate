using CleanArchitecture.Api.Middlewares;

namespace CleanArchitecture.Api;
internal static class DependencyInjectionApi
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddExceptionHandler<InvalidFormatExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
