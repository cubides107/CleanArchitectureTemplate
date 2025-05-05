using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Specifications;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Commands.Login;
public sealed class LoginUserCommandHandler(
    IPasswordHasher passwordHasher, IUserRepository userRepository,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<LoginUserDto>>
{
    public async Task<Result<LoginUserDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FirstOrDefaultAsync(new UserByEmailSpec(command.Email), cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginUserDto>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<LoginUserDto>(UserErrors.NotFoundByEmail);
        }

        string token = tokenProvider.Create(user);

        return new LoginUserDto(token);
    }
}
