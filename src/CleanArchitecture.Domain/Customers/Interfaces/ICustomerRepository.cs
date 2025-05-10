using CleanArchitecture.Domain.Customers.Entities;
using CleanArchitecture.Domain.Users.Interfaces;

namespace CleanArchitecture.Domain.Customers.Interfaces;
public interface ICustomerRepository : IRepository<Customer>
{
}
