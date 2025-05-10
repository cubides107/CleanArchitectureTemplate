using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Api.Customers.Commands.Create;
public record CreateCustomerCommand(string FirstName, string LastName, string Email, string PhoneNumber,
    Document IdentityDocument, CustomerAddress Address) : IRequest<Result>;

public record CustomerAddress(string Street, string City, string State, string Country, string ZipCode);

