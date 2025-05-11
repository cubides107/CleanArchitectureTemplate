namespace CleanArchitecture.Domain.Common.Ports;

public interface IIntegrationEvent
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
