using CleanArchitecture.Domain.Common.Ports;
using CleanArchitecture.Domain.Products.Entities;
using Refit;

namespace CleanArchitecture.Infrastructure.Ports;
public class InventoryApi(IRefitInventoryApi refitInventoryApi) : IInventoryApi
{
    public async Task<Product> GetPostAsync(int id)
    {
        ApiResponse<Product> products = await refitInventoryApi.GetProductById(id);
        return products.Content;
    }
}
