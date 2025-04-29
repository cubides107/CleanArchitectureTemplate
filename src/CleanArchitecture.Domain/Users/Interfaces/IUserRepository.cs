using CleanArchitecture.Domain.Users.Dtos;
using CleanArchitecture.Domain.Users.Entities;

namespace CleanArchitecture.Domain.Users.Interfaces;
public interface IUserRepository : IRepository<User>
{
    Task<List<UserPermission>> GetUserPermissionsByQuery(Guid userId);
}
