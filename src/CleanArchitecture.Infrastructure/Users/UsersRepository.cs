using Ardalis.Specification.EntityFrameworkCore;
using CleanArchitecture.Domain.Users.Dtos;
using CleanArchitecture.Domain.Users.Entities;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Users;
[Repository]
public class UsersRepository : RepositoryBase<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<UserPermission>> GetUserPermissionsByQuery(Guid userId)
    {
        FormattableString query = $"""
        SELECT DISTINCT
            u.id AS User_Id,
            rp.permission_code AS Permissions
        FROM users u
        JOIN user_roles ur ON ur.user_id = u.id
        JOIN role_permissions rp ON rp.role_name = ur.role_name
        WHERE u.id = {userId}
        """;
        return _context.Database.SqlQuery<UserPermission>(query).ToListAsync();
    }
}
