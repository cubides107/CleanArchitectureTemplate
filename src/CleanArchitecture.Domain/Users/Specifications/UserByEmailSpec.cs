using Ardalis.Specification;
using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.Domain.Users.Specifications;
public class UserByEmailSpec : Specification<User>
{
    public UserByEmailSpec(string email)
    {
        Query.Where(user => user.Email == new Email(email));
    }
}
