using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.Application.Abstractions.Authentication;
public interface ITokenProvider
{
    string Create(User user);
}
