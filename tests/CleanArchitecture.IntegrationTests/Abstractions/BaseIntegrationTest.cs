using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CleanArchitecture.Application.Users.Commands.Login;
using CleanArchitecture.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanArchitecture.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly HttpClient HttpClient;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        HttpClient = factory.CreateClient();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }

    public async Task GetToken()
    {

        var request = new LoginUserCommand("admin@gmail.com", "12345");
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("users/login", request);
        response.EnsureSuccessStatusCode();
        string token = await response.Content.ReadAsStringAsync();
        LoginUserDto? loginDto = JsonSerializer.Deserialize<LoginUserDto>(token);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginDto?.token);
    }
}
