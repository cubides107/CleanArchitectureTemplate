using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Orders.Entities;
public class Order : Entity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal Total { get; private set; }
    public List<OrderDetail> Details { get; private set; }

    public Order()
    {
        
    }

    public static Order Create(Guid id, Guid customerId)
    {
        return new Order()
        {
            Id = id,
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            Total = 0,
            Details = []
        };
    }
}
