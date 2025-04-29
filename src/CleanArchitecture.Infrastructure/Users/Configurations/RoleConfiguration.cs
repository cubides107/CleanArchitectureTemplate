using CleanArchitecture.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Users.Configurations;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Name);

        builder.Property(r => r.Name).HasMaxLength(50);

        builder
            .HasMany<User>()
            .WithMany(u => u.Roles)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("user_roles");

                joinBuilder.Property("RolesName").HasColumnName("role_name");
                joinBuilder.HasData(
                    // Member permissions
                    // Admin permissions
                    CreateUserRole(User.UserAdmin.Id, Role.Administrator.Name));
            });

        builder.HasData(
            Role.Member,
            Role.Administrator);
    }

    private static object CreateUserRole(Guid userId, string role)
    {
        return new
        {
            UserId = userId,
            RolesName = role
        };
    }
}
