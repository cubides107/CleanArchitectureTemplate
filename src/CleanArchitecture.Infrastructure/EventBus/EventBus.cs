using CleanArchitecture.Domain.Common.Ports;
using MassTransit;

namespace CleanArchitecture.Infrastructure.EventBus;
public class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}
