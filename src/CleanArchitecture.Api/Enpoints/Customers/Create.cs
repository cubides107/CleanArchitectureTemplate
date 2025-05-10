
using CleanArchitecture.Api.Enpoints.Users;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Infrastructure;
using CleanArchitecture.Application.Api.Customers.Commands.Create;
using CleanArchitecture.Domain.Common.SharedKernel;
using MediatR;

namespace CleanArchitecture.Api.Enpoints.Customers;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (CreateCustomerCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(request, cancellationToken);
            return result.Match(Results.Created, CustomResults.Problem);
        })
       .HasPermission(Permissions.UsersCreate)
       .WithTags(Tags.Customers);
    }
}
