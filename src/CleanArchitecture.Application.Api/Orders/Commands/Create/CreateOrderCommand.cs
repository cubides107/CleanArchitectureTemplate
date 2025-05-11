using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Orders.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Api.Orders.Commands.Create;
public record CreateOrderCommand(List<CreateOrderDto> OrderDetails, Guid CustomerId, Address ShippingAddress): IRequest<Result>;

