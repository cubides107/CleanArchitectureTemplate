using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Users.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(p => p.Email)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.HasData(User.UserAdmin);
    }
}
