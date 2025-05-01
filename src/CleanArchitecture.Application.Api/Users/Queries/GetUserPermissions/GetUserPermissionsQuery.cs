using CleanArchitecture.Application.Abstractions.Authorization;
using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Application.Api.Users.Queries.GetUserPermissions;
public sealed record GetUserPermissionsQuery(string IdentityId) : IRequest<Result<PermissionsResponse>>;
