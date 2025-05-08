using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Orders.Events;
using CleanArchitecture.Domain.Orders.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Worker.Orders.OrderCreatedEvent;
internal sealed class OrderCreatedEventHandler(IOrderRepository orderRepository) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Order order = await orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);

        Console.WriteLine($"Orden con id: {order!.Id}, {order.Total}, {order.CustomerId} Creada");
    }
}
