using System.Globalization;
using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Events;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Interfaces.Authentication;
using CleanArchitecture.Domain.Users.Specifications;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.Register;
internal sealed class RegisterUserCommandHandler(IUserRepository repository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var email = new Email(command.Email);

        var spec = new UserByEmailSpec(email);

        if (await repository.AnyAsync(spec, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        string passwordHash = passwordHasher.Hash(command.Password);

        var user = User.Create(new Email(command.Email), command.FirstName, command.LastName, passwordHash);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        await repository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}
