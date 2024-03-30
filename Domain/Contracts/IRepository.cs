using Domain.Base;

namespace Domain.Contracts;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetByID(long entityId);
    Task<bool> Add(T entity);
    Task<bool> Delete(long entityId);
    Task<bool> Update(T entity);
    Task<bool> SaveChanges();
}
