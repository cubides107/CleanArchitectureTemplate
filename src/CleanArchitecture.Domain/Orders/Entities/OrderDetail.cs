using CleanArchitecture.Domain.Common.Exceptions;
using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Orders.Entities;
public class OrderDetail : Entity
{
    private const int MaxQuantity = 10;

    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;

    public OrderDetail(){}

    public static OrderDetail Create(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ValidateQuantity(quantity);
        return new OrderDetail()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductName = productName,
            Quantity = quantity,
            UnitPrice = unitPrice,
        };
    }

    private static void ValidateQuantity(int quantity)
    {
        if(quantity <= 0)
        {
            throw new InvalidFormatException($"{nameof(Quantity)} must be greater than 0");
        }

        if (quantity > MaxQuantity)
        {
            throw new InvalidFormatException($"{nameof(Quantity)} must not exceed the maximum allowed.");
        }
    }
}
