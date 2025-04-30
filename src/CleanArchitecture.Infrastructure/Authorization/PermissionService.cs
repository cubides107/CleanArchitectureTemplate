using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Application.Api.Users.GetUserPermissions;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Infrastructure.Authorization;
internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
