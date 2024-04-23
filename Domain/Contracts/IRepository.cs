using Domain.Base;

namespace Domain.Contracts;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> Query { get; }
    Task<T> GetByID(long entityId);
    Task<bool> Add(T entity);
    Task<bool> Delete(long entityId);
    Task<bool> Update(T entity);
    Task<bool> SaveChanges();
}
