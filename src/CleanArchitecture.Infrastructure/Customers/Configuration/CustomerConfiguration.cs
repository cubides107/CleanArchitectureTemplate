using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Customers.Configuration;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Email)
            .HasConversion(email => email.Value, value => new Email(value));
        builder.ComplexProperty(property => property.Address);
        builder.ComplexProperty(property => property.IdentityDocument);
    }
}
