using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Domain.Users.Entities;
public class User : Entity
{
    public static readonly User UserAdmin = new()
    {
        Id = Guid.Parse("b3e8087e-975d-4a35-b9ad-6b8d7c41a550"),
        Email = new Email("admin@gmail.com"),
        FirstName = "admin",
        LastName = "admin",
        PasswordHash = "DF637DD4C6ACB86DEBC9401145F9B0C7C112CD94B4E454EAD510B0CE035CB044-DBC218C4173C96DE41CCCD8A77A1CC5B"
    };

    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PasswordHash { get; set; }

    private readonly List<Role> _roles = [];

    public IReadOnlyCollection<Role> Roles => [.. _roles];

    private User() { }

    public static User Create(Email email, string firstName, string lastName, string passwordHash)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = passwordHash
        };

        return user;
    }
}
