using CleanArchitecture.Domain.Orders.Services;
using MediatR;

namespace CleanArchitecture.Application.Api.Orders.Commands.Create;
public class CreateOrderHandler(CreateOrderService createOrderService) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await createOrderService.Create();
    }
}
