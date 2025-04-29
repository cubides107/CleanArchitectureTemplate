using Ardalis.Specification;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Domain.Users.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : Entity
{
}
