using Ardalis.Specification;
using CleanArchitecture.Domain.Common.ValueObjects;
using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.Domain.Users.Specifications;
public class UserByEmailSpec : Specification<User>
{
    public UserByEmailSpec(Email email)
    {
        Query.Where(user => user.Email == email);
    }
}
