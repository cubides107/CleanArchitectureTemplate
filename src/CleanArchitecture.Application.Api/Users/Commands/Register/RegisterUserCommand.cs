using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Commands.Register;
public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : IRequest<Result<Guid>>;
