using CleanArchitecture.Domain.Common.SharedKernel;
using CleanArchitecture.Domain.Customers.ValueObjects;

namespace CleanArchitecture.Domain.Customers.Errors;
public static class CustomerErrors
{
    public static Error NotFound(string customerId) => Error.NotFound(
          "Customer.NotFound",
          $"The customer with the Id = '{customerId}' was not found");

    public static Error CustomerAlreadyExists(Document document) => Error.Conflict(
        "Customer.AlreadyExists",
        $"A customer with the document ID '{document.Value}' already exists.");
}
