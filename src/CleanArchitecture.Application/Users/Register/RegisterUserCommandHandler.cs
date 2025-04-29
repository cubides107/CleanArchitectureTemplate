using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Domain.Users.Specifications;
using CleanArchitecture.SharedKernel;
using Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Users.Register;
internal sealed class RegisterUserCommandHandler(IUserRepository repository,
    IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(new UserByEmailSpec(command.Email), cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        string passwordHash = passwordHasher.Hash(command.Password);
        
        var user = User.Create(command.Email, command.FirstName, command.LastName, passwordHash);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        await repository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
