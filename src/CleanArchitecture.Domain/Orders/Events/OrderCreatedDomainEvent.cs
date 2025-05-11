using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Orders.Events;

public record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccurredOnUtc => DateTime.UtcNow;


    public Guid OrderId { get; init; } = OrderId ;
}
