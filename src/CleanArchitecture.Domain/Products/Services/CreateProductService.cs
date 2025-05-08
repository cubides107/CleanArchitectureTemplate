using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Domain.Products.Interfaces;

namespace CleanArchitecture.Domain.Products.Services;

[DomainService]
public class CreateProductService(
    IProductRepository productRepository)
{
    public async Task Create(string name, string description, decimal price, int stock)
    {
        var product = Product.Create(Guid.NewGuid(), name, description, price, stock);
        await productRepository.AddAsync(product);
    }
}
