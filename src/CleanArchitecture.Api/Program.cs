using System.Globalization;
using System.Reflection;
using CleanArchitecture.Api;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Application.Api.Users;
using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Localization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services
    .AddNotificationHandlers(typeof(DependencyInjectionApplication).Assembly)
    .AddApi()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

CultureInfo[] cultures =
[
    new CultureInfo("es-CO"),
];

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures[0]);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.UseRequestLocalization();

app.MapEndpoints();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

await app.RunAsync();
