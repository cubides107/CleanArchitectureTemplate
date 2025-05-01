using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Domain.Users.Dtos;
using CleanArchitecture.Domain.Users.Errors;
using CleanArchitecture.Domain.Users.Interfaces;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Queries.GetUserPermissions;
internal sealed class GetUserPermissionsQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserPermissionsQuery, Result<PermissionsResponse>>
{
    public async Task<Result<PermissionsResponse>> Handle(
        GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.IdentityId);
        List<UserPermission> permissions = await userRepository.GetUserPermissionsByQuery(id);

        if (!permissions.Any())
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId.ToString()));
        }

        return new PermissionsResponse(permissions[0].UserId, permissions.Select(p => p.Permissions).ToHashSet());
    }
}
