using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.Errors;
using CleanArchitecture.Domain.Customers.Interfaces;
using CleanArchitecture.Domain.Customers.Specifications;
using CleanArchitecture.Domain.Customers.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Api.Customers.Commands.Create;
public class CreateCustomerHandler(ICustomerRepository customerRepository)
    : IRequestHandler<CreateCustomerCommand, Result>
{
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var document = new Document(request.IdentityDocument.Type, request.IdentityDocument.Value);
        bool exitsCustomer = await customerRepository.AnyAsync(new ExistsCustomerSpec(document), cancellationToken);

        if (exitsCustomer)
        {
            return Result.Failure(CustomerErrors.CustomerAlreadyExists(document));
        }

        var address = Address.Create(request.Address.Street, request.Address.City, request.Address.State,
            request.Address.Country, request.Address.ZipCode);

        var email = new Email(request.Email);

        var customer = Customer.Create(request.FirstName, request.LastName,
            email, request.PhoneNumber, document, address);

        await customerRepository.AddAsync(customer, cancellationToken);
        return Result.Success();
    }
}
