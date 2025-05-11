using CleanArchitecture.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Orders.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(p => p.ShippingAddress);

        builder.Navigation(p => p.Details).AutoInclude();
    }
}
