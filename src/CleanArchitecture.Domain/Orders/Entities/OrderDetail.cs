using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Orders.Entities;
public class OrderDetail : Entity
{
    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;

    public OrderDetail(){}

    public OrderDetail Create(Guid productId, string productName, int Quantity, decimal UnitPrice)
    {
        return new OrderDetail()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductName = productName,
            Quantity = Quantity,
            UnitPrice = UnitPrice,
        };
    }
}
