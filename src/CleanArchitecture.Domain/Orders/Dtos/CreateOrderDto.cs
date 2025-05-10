namespace CleanArchitecture.Domain.Orders.Dtos;
public record CreateOrderDto(Guid ProductId, string ProductName, int Quantity);
