using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.Domain.Users.Interfaces.Authentication;
public interface ITokenProvider
{
    string Create(User user);
}
