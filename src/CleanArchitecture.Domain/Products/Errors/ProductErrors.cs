using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Products.Errors;
public static class ProductErrors
{
    public static Error NotFound(string productId) => Error.NotFound(
            "Product.NotFound",
            $"The product with the Id = '{productId}' was not found");
}
