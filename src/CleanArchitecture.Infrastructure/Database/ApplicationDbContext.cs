using CleanArchitecture.Domain.Common.Interfaces;
using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Database;
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<Order> Orders { get; set; }
    internal DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
