using CleanArchitecture.Domain.Orders.Entities;
using CleanArchitecture.Domain.Users.Interfaces;

namespace CleanArchitecture.Domain.Orders.Interfaces;
public interface IOrderRepository: IRepository<Order>
{
}
