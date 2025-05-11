using CleanArchitecture.Domain.Products.Entities;
using Refit;

namespace CleanArchitecture.Infrastructure.Ports;
public interface IRefitInventoryApi
{
    [Get("/posts/{id}")]
    Task<ApiResponse<Product>> GetProductById(int id);
}
