using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Authorization;
internal sealed class PermissionAuthorizationHandler(IPermissionService permissionService)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {

        Result<PermissionsResponse> permissions = await permissionService.GetUserPermissionsAsync(context.User.GetIdentityId());

        if (permissions.Value.Permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
