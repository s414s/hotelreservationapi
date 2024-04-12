using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Implementations;

public class GuestsRepository : IRepository<Guest>
{
    private readonly DatabaseContext _context;

    public GuestsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Guest> Query => _context.Guests;

    public async Task<bool> Add(Guest entity)
    {
        await _context.Guests.AddAsync(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(long entityId)
    {
        var entity = await GetByID(entityId);
        if (entity == null)
        {
            return true;
        }
        _context.Guests.Remove(entity);
        return await SaveChanges();
    }

    public async Task<IEnumerable<Guest>> GetAll()
    {
        return await _context.Guests.ToListAsync();
    }

    public async Task<Guest> GetByID(long entityId)
    {
        return await _context.Guests.SingleAsync(x => x.Id == entityId);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Guest entity)
    {
        _context.Guests.Update(entity);
        return await SaveChanges();
    }
}
