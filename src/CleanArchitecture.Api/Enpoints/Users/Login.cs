using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Infrastructure;
using CleanArchitecture.Application.Api.Users.Commands.Login;
using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Api.Enpoints.Users;
public sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (LoginUserCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<LoginUserDto> result = await sender.Send(request, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
