using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Users.Services;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Commands.Login;
public sealed class LoginUserCommandHandler(
   LoginService loginService) : IRequestHandler<LoginUserCommand, Result<LoginUserDto>>
{
    public async Task<Result<LoginUserDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        Result<string> token = await loginService.Login(command.Email, command.Password, cancellationToken);
        if (token.IsFailure)
        {
            return Result.Failure<LoginUserDto>(token.Error);
        }
        return new LoginUserDto(token.Value);
    }
}
