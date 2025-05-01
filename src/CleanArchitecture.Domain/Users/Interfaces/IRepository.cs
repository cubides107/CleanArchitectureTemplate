using Ardalis.Specification;
using CleanArchitecture.Domain.Common.SharedKernel;

namespace CleanArchitecture.Domain.Users.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : Entity
{
}
