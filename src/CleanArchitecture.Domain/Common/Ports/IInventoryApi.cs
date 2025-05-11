using CleanArchitecture.Domain.Products.Entities;

namespace CleanArchitecture.Domain.Common.Ports;
public interface IInventoryApi
{
    Task<Product> GetPostAsync(int id);
}
