using System.Net.Http.Json;
using CleanArchitecture.Application.Api.Users.Commands.Register;
using CleanArchitecture.IntegrationTests.Abstractions;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
        var request = new RegisterUserCommand("crist","test", "ews234", "123");

        // Act
        await GetToken();
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("users/register", request);
        response.EnsureSuccessStatusCode();
        string userId = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.IsNotNull(userId);
    }
}
