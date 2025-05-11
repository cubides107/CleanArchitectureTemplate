using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Orders.Enums;

namespace CleanArchitecture.Domain.Orders.Entities;
public class Order : Entity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public Address ShippingAddress { get; private set; }
    public string? TrackingCode { get; private set; }
    public decimal Total { get; private set; }
    public Guid? PaymentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderDetail> Details { get; private set; }

    public Order()
    {

    }

    public void AddOrderDetail(OrderDetail orderDetail)
    {
        Details.Add(orderDetail);
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        Total = Details.Sum(orderDetail => orderDetail.Subtotal);
    }

    public static Order Create(Guid id, Guid customerId, Address shippingAddress)
    {
        return new Order()
        {
            Id = id,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            CreatedAt = DateTime.UtcNow,
            PaymentId = default!,
            OrderStatus = OrderStatus.Pending,
            Total = 0,
            Details = [],
        };
    }
}
