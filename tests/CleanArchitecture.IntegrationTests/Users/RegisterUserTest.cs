using System.Net.Http.Json;
using CleanArchitecture.Application.Users.Commands.Register;
using CleanArchitecture.IntegrationTests.Abstractions;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.IntegrationTests.Users;
public class RegisterUserTest : BaseIntegrationTest
{
    public RegisterUserTest(IntegrationTestWebAppFactory factory): base(factory)
    {
        
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenRequestIsNotValid()
    {
        // Arrange
        var request = new RegisterUserCommand("cristian@gmail.com","test", "ews234", "123");

        // Act
        await GetToken();
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("users/register", request);
        response.EnsureSuccessStatusCode();
        string userId = await response.Content.ReadAsStringAsync();

        //Assert
        userId.Should().NotBeNullOrEmpty();
    }
}
