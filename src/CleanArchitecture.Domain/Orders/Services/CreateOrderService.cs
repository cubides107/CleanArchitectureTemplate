using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Orders.Events;
using CleanArchitecture.Domain.Orders.Interfaces;

namespace CleanArchitecture.Domain.Orders.Services;

[DomainService]
public class CreateOrderService(IOrderRepository orderRepository)
{
    public async Task Create()
    {
        var order = Order.Create(Guid.NewGuid(), Guid.NewGuid());

        order.Raise(new OrderCreatedDomainEvent(order.Id));

        await orderRepository.AddAsync(order);
    }
}
