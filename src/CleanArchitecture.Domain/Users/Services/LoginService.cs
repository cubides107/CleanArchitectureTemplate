using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Interfaces.Authentication;
using CleanArchitecture.Domain.Users.Specifications;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Domain.Users.Services;

[DomainService]
public class LoginService(
    IUserRepository userRepository,
    ITokenProvider tokenProvider, 
    IPasswordHasher passwordHasher)
{
    public async Task<Result<string>> Login(string email, string password, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync(new UserByEmailSpec(new Email(email)), cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        return tokenProvider.Create(user);
    }
}
