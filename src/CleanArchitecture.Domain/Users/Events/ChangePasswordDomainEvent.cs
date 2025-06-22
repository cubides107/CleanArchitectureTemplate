using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Users.Events;
public record ChangePasswordDomainEvent(Guid UserId)
    : IDomainEvent
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
