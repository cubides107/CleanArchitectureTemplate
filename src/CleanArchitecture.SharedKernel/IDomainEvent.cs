using MediatR;

namespace CleanArchitecture.SharedKernel;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
