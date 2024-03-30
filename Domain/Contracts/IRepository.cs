using Domain.Base;

namespace Domain.Contracts;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetByID(Guid id);
    Task<bool> Add(T entity);
    Task<bool> Delete(T entity);
    Task<bool> Update(T entity);
    Task<bool> SaveChanges();
}
