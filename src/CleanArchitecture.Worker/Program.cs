using CleanArchitecture.Application.Worker.UserRegisterEvent;
using CleanArchitecture.Domain.Users.Events;
using CleanArchitecture.Worker.Consumers;
using CleanArchitecture.Worker.Data;
using CleanArchitecture.Worker.EventBus;
using CleanArchitecture.Worker.Outbox;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Quartz;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionString("Queue")!);

builder.Services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

builder.Services.AddScoped<INotificationHandler<UserRegisteredDomainEvent>, UserRegisteredDomainEventHandler>();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.Configure<OutboxOptions>(builder.Configuration.GetSection("Outbox"));

builder.Services.AddQuartz(configurator =>
{
    var scheduler = Guid.NewGuid();
    configurator.SchedulerId = $"default-id-{scheduler}";
    configurator.SchedulerName = $"default-name-{scheduler}";
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
builder.Services.ConfigureOptions<ConfigureProcessOutboxJob>();


builder.Services.AddSingleton(sp =>
{
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("ApplicationDatabase"));
    return dataSourceBuilder.Build();
});

builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    configure.AddConsumer<OutboxProcess>();

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

IHost host = builder.Build();
host.Run();
