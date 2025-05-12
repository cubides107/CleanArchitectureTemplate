using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Interfaces.Authentication;
using CleanArchitecture.Domain.Users.Services;
using CleanArchitecture.Domain.Users.Specifications;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.UnitTests.Users.Builders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CleanArchitecture.UnitTests.Users.Services;

public class LoginServiceTests
{
    private readonly LoginService _loginService;
    private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly ITokenProvider tokenProvider = Substitute.For<ITokenProvider>();

    public LoginServiceTests()
    {
        _loginService = new LoginService(userRepository, tokenProvider, passwordHasher);
    }

    [Fact]
    public async Task Login_WhenCredentialAreCorrect_ShouldReturnToken()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";
        var user = User.Create(new Email(email), "cristian", "cubides", "HSDFHS2342");

        userRepository.FirstOrDefaultAsync(Arg.Any<UserByEmailSpec>()).Returns(user);
        passwordHasher.Verify(password, "HSDFHS2342").Returns(true);
        tokenProvider.Create(user).Returns("JADFSDFAF234234VSDFAFfsdfsf");

        //Act
        Result<string> token = await _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Value.Should().Be("JADFSDFAF234234VSDFAFfsdfsf");
    }

    [Fact]
    public async Task Login_WhenUserNoExits_ShouldReturnNotFoundByEmail()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";

        userRepository.FirstOrDefaultAsync(new UserByEmailSpec(email)).ReturnsNull();

        //Act
        Result<string> token = await _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Error.Should().Be(UserErrors.NotFoundByEmail);
    }

    [Fact]
    public async Task Login_WhenCredentialIncorrect_ShouldReturnNotFoundByEmail()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";
        User user = new UserBuilder().WithEmail(email).WithPasswordHash("JA#4SDF34").Build();

        userRepository.FirstOrDefaultAsync(Arg.Any<UserByEmailSpec>()).Returns(user);
        passwordHasher.Verify(password, "HSDFHS2342").Returns(false);

        //Act
        Result<string> token = await _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Error.Should().Be(UserErrors.NotFoundByEmail);
    }
}
