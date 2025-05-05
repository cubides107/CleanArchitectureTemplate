using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Infrastructure;
using CleanArchitecture.Application.Api.Users.Commands.Register;
using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Api.Enpoints.Users;
public sealed class Register : IEndpoint
{
    internal sealed record Request(string Email, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersCreate)
        .WithTags(Tags.Users);
    }
}
