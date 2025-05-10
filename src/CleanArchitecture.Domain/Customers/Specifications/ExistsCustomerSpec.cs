using Ardalis.Specification;
using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Customers.ValueObjects;

namespace CleanArchitecture.Domain.Customers.Specifications;
public class ExistsCustomerSpec : Specification<Customer> 
{
    public ExistsCustomerSpec(Document document)
    {
        Query.Where(customer => customer.IdentityDocument == document);
    }
}
