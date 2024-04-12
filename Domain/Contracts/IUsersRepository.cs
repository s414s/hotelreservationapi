using Domain.Entities;

namespace Domain.Contracts;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> GetByCredentials(string username, string password);
}
