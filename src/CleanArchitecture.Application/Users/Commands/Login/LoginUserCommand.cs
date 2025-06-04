using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginUserDto>>;
