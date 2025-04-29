using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Application.Abstractions.Authorization;
public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
