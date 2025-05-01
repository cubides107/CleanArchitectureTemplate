using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Infrastructure;
using CleanArchitecture.Application.Users.Commands.Register;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace CleanArchitecture.Api.Enpoints.Users;
internal sealed class Register : IEndpoint
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
