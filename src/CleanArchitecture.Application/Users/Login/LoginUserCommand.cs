using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;
