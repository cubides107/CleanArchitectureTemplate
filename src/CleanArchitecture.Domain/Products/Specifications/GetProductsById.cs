using Ardalis.Specification;
using CleanArchitecture.Domain.Products.Entities;

namespace CleanArchitecture.Domain.Products.Specifications;
public class GetProductsById : Specification<Product>
{
    public GetProductsById(List<Guid> productsId)
    {
        Query.Where(product => productsId.Contains(product.Id)); 
    }
}
