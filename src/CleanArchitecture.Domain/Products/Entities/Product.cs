using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Products.Entities;
public class Product : Entity
{
    public static readonly Product Product1 = Create(Guid.Parse("494a3029-c45e-48b2-b646-a5792867c237"), "Producto 1", "Producto de Prueba", 10000, 2);
    public static readonly Product Product2 = Create(Guid.Parse("1e45d172-b493-43a9-8c5e-f50cb9409c6f"), "Producto 2", "Producto de Prueba", 5000, 4);
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
