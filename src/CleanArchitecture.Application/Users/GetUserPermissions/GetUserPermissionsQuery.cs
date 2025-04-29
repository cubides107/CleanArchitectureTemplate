using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace Evently.Modules.Users.Application.Users.GetUserPermissions;
public sealed record GetUserPermissionsQuery(string IdentityId) : IRequest<Result<PermissionsResponse>>;
