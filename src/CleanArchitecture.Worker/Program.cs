using CleanArchitecture.Application.Worker;
using CleanArchitecture.Infrastructure;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddApplicationWorker()
    .AddInfrastructureWorker(builder.Configuration);

IHost host = builder.Build();
host.Run();
