using CleanArchitecture.SharedKernel;

namespace Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent
{
    public Guid Id => UserId;

    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
