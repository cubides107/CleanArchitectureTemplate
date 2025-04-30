using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.GetUserPermissions;
public sealed record GetUserPermissionsQuery(string IdentityId) : IRequest<Result<PermissionsResponse>>;
