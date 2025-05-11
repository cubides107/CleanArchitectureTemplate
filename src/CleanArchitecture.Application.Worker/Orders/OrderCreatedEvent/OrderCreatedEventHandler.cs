using CleanArchitecture.Application.Abstractions.Exceptions;
using CleanArchitecture.Domain.Common.Ports;
using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Orders.Events;
using CleanArchitecture.Domain.Orders.Events.IntegrationEvents;
using CleanArchitecture.Domain.Orders.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Worker.Orders.OrderCreatedEvent;
internal sealed class OrderCreatedEventHandler(
    IOrderRepository orderRepository, IEventBus bus) 
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Order order = await orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);

        if(order is null)
        {
            throw new AppException($"Not found order created by Id {notification.OrderId}");
        }

        List<OrderItemDto> orderItemDtos = [.. order.Details.Select(order => new OrderItemDto(order.ProductId, order.Quantity))];

        var orderCreatedEvent = new OrderCreatedIntegrationEvent(Guid.NewGuid(), DateTime.UtcNow, order.Id, orderItemDtos);
        
        await bus.PublishAsync(orderCreatedEvent, cancellationToken);
        Console.WriteLine($"Orden con id: {order!.Id}, {order.Total}, {order.CustomerId} Creada");
    }
}
