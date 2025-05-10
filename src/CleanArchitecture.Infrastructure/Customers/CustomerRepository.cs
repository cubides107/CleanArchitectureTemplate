using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.Interfaces;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Customers;

[Repository]
public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
