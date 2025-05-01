using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Commands.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;
