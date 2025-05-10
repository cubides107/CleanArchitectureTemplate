using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.UnitTests.Users.Builders;
internal sealed class UserBuilder
{
    private string _email = "default@example.com";
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _passwordHash = "defaultHash";

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UserBuilder WithPasswordHash(string passwordHash)
    {
        _passwordHash = passwordHash;
        return this;
    }

    public User Build()
    {
        return User.Create(new Email(_email), _firstName, _lastName, _passwordHash);
    }
}
