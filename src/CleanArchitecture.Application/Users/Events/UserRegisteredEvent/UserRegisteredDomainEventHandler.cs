using CleanArchitecture.Domain.Users.Events;
using MediatR;

namespace CleanArchitecture.Application.Users.Events.UserRegisteredEvent;

[WorkerHandler]
public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredDomainEvent request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Ejecucion Usuario registrado");
        return Task.CompletedTask;
    }
}
