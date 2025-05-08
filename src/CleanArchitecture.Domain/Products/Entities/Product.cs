using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Products.Entities;
public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product()
    {

    }

    public static Product Create(Guid id, string name, string description, decimal price, int stock)
    {
        return new Product()
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            Stock = stock
        };
    }
}
