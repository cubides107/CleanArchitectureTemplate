using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Orders.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Api.Orders.Commands.Create;
public record CreateOrderCommand(List<CreateOrderDto> OrderDetails, Guid CustomerId): IRequest<Result>;

