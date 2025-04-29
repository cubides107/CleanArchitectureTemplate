namespace CleanArchitecture.Api.Extensions;

internal static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        return app;
    }
}
