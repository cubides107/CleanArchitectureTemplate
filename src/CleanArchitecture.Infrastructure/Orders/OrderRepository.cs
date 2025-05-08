using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Orders.Interfaces;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Orders;

[Repository]
public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
