using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Interfaces.Authentication;
using CleanArchitecture.Domain.Users.Services;
using CleanArchitecture.Domain.Users.Specifications;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanArchitecture.UnitTests.Users.Services;

[TestClass]
public class LoginServiceTests
{
    private LoginService _loginService = default!;
    private readonly IUserRepository userRepository = Substitute.For<IUserRepository>()!;
    private readonly IPasswordHasher passwordHasher = Substitute.For<IPasswordHasher>()!;
    private readonly ITokenProvider tokenProvider = Substitute.For<ITokenProvider>()!;

    [TestInitialize()]
    public void Initialize()
    {
        _loginService = new LoginService(userRepository, tokenProvider, passwordHasher);
    }

    [TestMethod]
    public async Task Login_WhenCredentialAreCorrect_ShouldReturnToken()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";
        var user = User.Create(email, "cristian", "cubides", "HSDFHS2342");

        userRepository.FirstOrDefaultAsync(Arg.Any<UserByEmailSpec>()).Returns(user);
        passwordHasher.Verify(password, "HSDFHS2342").Returns(true);
        tokenProvider.Create(user).Returns("JADFSDFAF234234VSDFAFfsdfsf");

        //Act
        Result<string> token = await _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Value.Should().Be("JADFSDFAF234234VSDFAFfsdfsf");
    }

    [TestMethod]
    public void Login_WhenUserNoExits_ShouldReturnNotFoundByEmail()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";

        userRepository.FirstOrDefaultAsync(new UserByEmailSpec(email)).ReturnsNull();

        //Act
        Task<Result<string>> token = _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }

    [TestMethod]
    public void Login_WhenCredentialIncorrect_ShouldReturnNotFoundByEmail()
    {
        //Arrange
        string email = "cristian@gmail.com";
        string password = "12345";
        var user = User.Create(email, "cristian.cubides@gmail.com", "cubides", "HSDFHS2342");

        userRepository.FirstOrDefaultAsync(Arg.Any<UserByEmailSpec>()).Returns(user);
        passwordHasher.Verify(password, "HSDFHS2342").Returns(false); 

        //Act
        Task<Result<string>> token = _loginService.Login(email, password, CancellationToken.None);

        //Assert
        token.Result.Error.Should().Be(UserErrors.NotFoundByEmail);
    }
}
