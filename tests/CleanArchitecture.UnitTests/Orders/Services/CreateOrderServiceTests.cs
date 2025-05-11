using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.Errors;
using CleanArchitecture.Domain.Customers.Interfaces;
using CleanArchitecture.Domain.Customers.ValueObjects;
using CleanArchitecture.Domain.Orders.Dtos;
using CleanArchitecture.Domain.Orders.Interfaces;
using CleanArchitecture.Domain.Orders.Services;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Domain.Products.Interfaces;
using CleanArchitecture.Domain.Products.Specifications;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CleanArchitecture.UnitTests.Orders.Services;
public class CreateOrderServiceTests
{
    private readonly CreateOrderService _orderService;
    private readonly IOrderRepository orderRepository = Substitute.For<IOrderRepository>();
    private readonly ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
    private readonly IProductRepository productRepository = Substitute.For<IProductRepository>();

    public CreateOrderServiceTests()
    {
        _orderService = new CreateOrderService(orderRepository, productRepository, customerRepository);
    }

    [Fact]
    public async Task Create_WhenOrderIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var shippingAddress = Address.Create("123 Main St", "City", "Boyacá", "Colombia", "10005");
        var productId = Guid.NewGuid();
        var document = new Document(DocumentType.CC,"12345678");
        var email = new Email("cristian.cubides@gmail.com");
        var createOrderDtos = new List<CreateOrderDto> { new(productId,"Test", 2) };
        var customer = Customer.Create("Cristian", "Cubides", email, "3176718273", document, shippingAddress);
        var product = Product.Create(productId, "Product A", "Test descripcion producto", 100, 2);

        customerRepository.GetByIdAsync(customerId).Returns(customer);
        productRepository.ListAsync(Arg.Any<GetProductsById>()).Returns([product]);

        // Act
        Result result = await _orderService.Create(createOrderDtos, customerId, shippingAddress);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Create_WhenCustomerNotFound_ShouldReturnFailure()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var shippingAddress = Address.Create("123 Main St", "City", "Boyacá", "Colombia", "10005");
        var createOrderDtos = new List<CreateOrderDto> { };

        customerRepository.GetByIdAsync(customerId).ReturnsNull();

        // Act
        Result result = await _orderService.Create(createOrderDtos, customerId, shippingAddress);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CustomerErrors.NotFound(customerId.ToString()));
    }
}
