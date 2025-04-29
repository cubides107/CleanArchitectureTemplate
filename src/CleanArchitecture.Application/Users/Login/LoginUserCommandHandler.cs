using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Specifications;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Login;
public sealed class LoginUserCommandHandler(
    IPasswordHasher passwordHasher, IUserRepository userRepository,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync(new UserByEmailSpec(command.Email), cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
