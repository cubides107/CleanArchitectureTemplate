using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent
{
    public Guid Id => UserId;

    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
