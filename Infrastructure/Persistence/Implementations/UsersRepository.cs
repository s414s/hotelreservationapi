using Domain.Contracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Implementations;

public class UsersRepository : IRepository<User>
{
    public IQueryable<User> Query => throw new NotImplementedException();

    public Task<bool> Add(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(long entityId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByID(long entityId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User entity)
    {
        throw new NotImplementedException();
    }
}
