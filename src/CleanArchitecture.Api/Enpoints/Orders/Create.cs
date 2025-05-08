
using CleanArchitecture.Api.Enpoints.Users;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Application.Api.Orders.Commands.Create;
using MediatR;

namespace CleanArchitecture.Api.Enpoints.Orders;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (CreateOrderCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(request, cancellationToken);
            return Results.Ok();
        })
       .HasPermission(Permissions.UsersCreate)
       .WithTags(Tags.Orders);
    }
}
