using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Users.Queries.GetUserPermissions;
public sealed record GetUserPermissionsQuery(string IdentityId) : IRequest<Result<PermissionsResponse>>;
