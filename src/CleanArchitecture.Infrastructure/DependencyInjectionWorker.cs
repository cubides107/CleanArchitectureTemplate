
using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Domain.Orders.Interfaces;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.EventBus;
using CleanArchitecture.Infrastructure.EventBus.Consumers;
using CleanArchitecture.Infrastructure.Orders;
using CleanArchitecture.Infrastructure.Outbox;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using CleanArchitecture.Domain.Common.Ports;
using CleanArchitecture.Domain.Orders.Events.IntegrationEvents;

namespace CleanArchitecture.Infrastructure;
public static class DependencyInjectionWorker
{
    public static IServiceCollection AddInfrastructureWorker(this IServiceCollection services,
           IConfiguration configuration)
    {
        services
             .AddServices(configuration)
             .AddDatabaseEF(configuration)
             .AddContextDapper(configuration)
             .AddEventBus(configuration)
             .AddQuartz();
        return services;
    }

    private static IServiceCollection AddDatabaseEF(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("ApplicationDatabase");

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());
        return services;
    }

    private static IServiceCollection AddContextDapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("ApplicationDatabase"));
            return dataSourceBuilder.Build();
        });
        return services;
    }

    private static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = new RabbitMqSettings(configuration.GetConnectionString("Queue")!);
        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqSettings.Host), h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }

    
    private static IServiceCollection AddQuartz(this IServiceCollection services)
    {
        services.AddQuartz(configurator =>
         {
             var scheduler = Guid.NewGuid();
             configurator.SchedulerId = $"default-id-{scheduler}";
             configurator.SchedulerName = $"default-name-{scheduler}";
         });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();
        services.Configure<OutboxOptions>(configuration.GetSection("Messaging:Outbox"));
        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }

}
