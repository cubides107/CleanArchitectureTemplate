using CleanArchitecture.Domain.Common.Extensions;
using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Users.Services;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Commands.Login;
public sealed class LoginUserCommandHandler(
   LoginService loginService) : IRequestHandler<LoginUserCommand, Result<LoginUserDto>>
{
    public async Task<Result<LoginUserDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        return await loginService
            .Login(command.Email, command.Password, cancellationToken)
            .MapAsync(token => new LoginUserDto(token));
    }
}
