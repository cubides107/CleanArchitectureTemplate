using System.Text;
using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
             .AddServices()
             .AddRepositories()
             .AddAuthenticationInternal(configuration)
             .AddAuthorizationInternal()
             .AddDatabase(configuration)
             .AddDomainServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPermissionService, PermissionService>();
        return services;
    }


    private static IServiceCollection AddAuthenticationInternal(
           this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(o =>
           {
               o.RequireHttpsMetadata = false;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   ClockSkew = TimeSpan.Zero
               };
           });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("ApplicationDatabase");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName))
                .UseSnakeCaseNamingConvention());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        var domainServices = typeof(DomainServiceAttribute).Assembly
            .GetTypes()
            .Where(attribute => attribute.CustomAttributes.Any(service => service.AttributeType == typeof(DomainServiceAttribute)))
            .ToList();


        foreach (Type? service in domainServices)
        {
            Type? interfaceService = service.GetInterfaces().FirstOrDefault();

            if (interfaceService is null)
            {
                services.AddTransient(service);
            }
            else
            {
                services.AddTransient(interfaceService, service);
            }
        }

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var repositories = typeof(RepositoryAttribute).Assembly
            .GetTypes()
            .Where(attribute => attribute.CustomAttributes.Any(service => service.AttributeType == typeof(RepositoryAttribute)))
            .ToList();

        foreach (Type? service in repositories)
        {
            Type? interfaceService = service.GetInterfaces()
                .FirstOrDefault(@interface =>
                    @interface.Namespace != null &&
                    @interface.Namespace.StartsWith("CleanArchitecture.Domain", StringComparison.InvariantCulture) &&
                    !@interface.Name.Contains("Base") &&
                    !@interface.IsGenericType);

            if (interfaceService is null)
            {
                services.AddScoped(service);
            }
            else
            {
                services.AddScoped(interfaceService, service);
            }
        }

        return services;
    }
}
