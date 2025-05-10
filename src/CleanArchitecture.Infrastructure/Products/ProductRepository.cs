
using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Domain.Products.Interfaces;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Products;

[Repository]
public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
