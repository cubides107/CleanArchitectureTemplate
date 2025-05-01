using MediatR;

namespace CleanArchitecture.Domain.Common.SharedKernel;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
