using Domain.Entities;

namespace Application.Contracts;

public interface IGuestService
{
    Task<Guest> GetById(long id);
    Task<Guest> CreateNew();
}
