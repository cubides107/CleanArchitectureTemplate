using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.Errors;
using CleanArchitecture.Domain.Customers.Interfaces;
using CleanArchitecture.Domain.Orders.Dtos;
using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Orders.Events;
using CleanArchitecture.Domain.Orders.Interfaces;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Domain.Products.Errors;
using CleanArchitecture.Domain.Products.Interfaces;
using CleanArchitecture.Domain.Products.Specifications;

namespace CleanArchitecture.Domain.Orders.Services;

[DomainService]
public class CreateOrderService(
    IOrderRepository orderRepository, 
    IProductRepository productRepository,
    ICustomerRepository customerRepository)
{
    public async Task<Result> Create(List<CreateOrderDto> createOrderDtos, Guid customerId)
    {
        var order = Order.Create(Guid.NewGuid(), customerId);

        Customer? customer = await customerRepository.GetByIdAsync(customerId);

        if(customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(customerId.ToString()));
        }

        var productIds = createOrderDtos.Select(orderDetail => orderDetail.ProductId).ToList();

        var spec = new GetProductsById(productIds);
        List<Product> products = await productRepository.ListAsync(spec);

        foreach (CreateOrderDto orderDto in createOrderDtos)
        {
            Product? product = products.Find(product => product.Id == orderDto.ProductId);
            if (product is null)
            {
                return Result.Failure(ProductErrors.NotFound(orderDto.ProductId.ToString()));
            }
            var orderDetail = OrderDetail.Create(orderDto.ProductId, product.Name, orderDto.Quantity, product.Price);
            order.AddOrderDetail(orderDetail);
        }

        order.Raise(new OrderCreatedDomainEvent(order.Id));

        await orderRepository.AddAsync(order);
        return Result.Success();
    }
}
