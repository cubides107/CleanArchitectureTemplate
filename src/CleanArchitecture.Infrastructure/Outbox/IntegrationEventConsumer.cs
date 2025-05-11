using System.Text.Json;
using CleanArchitecture.Domain.Common.Ports;
using CleanArchitecture.Infrastructure.Serialization;
using MassTransit;

namespace CleanArchitecture.Infrastructure.Outbox;
internal sealed class IntegrationEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        string message = JsonSerializer.Serialize(context.Message, SerializerSettings.Instance);
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}
