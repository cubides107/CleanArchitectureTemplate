using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.Register;
public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : IRequest<Result<Guid>>;
