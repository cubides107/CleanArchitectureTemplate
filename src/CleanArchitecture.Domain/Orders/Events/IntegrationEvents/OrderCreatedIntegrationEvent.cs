using CleanArchitecture.Domain.Common.Ports;

namespace CleanArchitecture.Domain.Orders.Events.IntegrationEvents;
public sealed class OrderCreatedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; init; }
    public List<OrderItemDto> OrderItemDtos { get; init; }

    public OrderCreatedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid orderId,
        List<OrderItemDto> orderItemDtos)
        : base(id, occurredOnUtc)
    {
        OrderId = orderId;
        OrderItemDtos = orderItemDtos;
    }
}

public record OrderItemDto(Guid ProductId, int Quantity);
