using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Orders.Services;
using MediatR;

namespace CleanArchitecture.Application.Api.Orders.Commands.Create;
public class CreateOrderHandler(CreateOrderService createOrderService)
    : IRequestHandler<CreateOrderCommand, Result>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return await createOrderService.Create(request.OrderDetails, request.CustomerId, request.ShippingAddress);
    }
}
