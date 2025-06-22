using CleanArchitecture.Domain.Common.Exceptions;
using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Users.Events;

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
    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

    public List<Role> Roles { get; private set; } = [];

    private User() { }

    public static User Create(Email email, string firstName, string lastName, string passwordHash)
    {
        if (email is null)
        {
            throw new InvalidFormatException($"{nameof(Email)} cannot be null.");
        }
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidFormatException($"{nameof(FirstName)} cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new InvalidFormatException($"{nameof(LastName)} cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new InvalidFormatException($"{nameof(PasswordHash)} cannot be null or empty.");
        }
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = passwordHash,
            IsActive = true
        };
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
        {
            throw new InvalidFormatException($"{nameof(PasswordHash)} cannot be null or empty.");
        }
        PasswordHash = newPasswordHash;
        Raise(new ChangePasswordDomainEvent(Id));
    }

    public void ChangeName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidFormatException($"{nameof(FirstName)} cannot be null or empty.");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new InvalidFormatException($"{nameof(LastName)} cannot be null or empty.");
        }
        FirstName = firstName;
        LastName = lastName;
    }

    public void Activate()
    {
        if (IsActive)
        {
            return;
        }
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            return;
        }
        IsActive = false;
    }

    public void AddRole(Role role)
    {
        if (role is null)
        {
            throw new InvalidFormatException($"{nameof(Role)} cannot be null.");
        }
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }

    public void RemoveRole(Role role)
    {
        if (role is null)
        {
            throw new InvalidFormatException($"{nameof(Role)} cannot be null.");
        }
        if(role == Role.Administrator)
        {
            throw new InvalidFormatException("Can not remove the Administrator role"); 
        }

        if (!Roles.Contains(role))
        {
            return;
        }
        Roles.Remove(role);
    }

    public bool HasRole(Role role) => Roles.Contains(role);

    public bool CheckPassword(string passwordHash) => PasswordHash == passwordHash;
}
