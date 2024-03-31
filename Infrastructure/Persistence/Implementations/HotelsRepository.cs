using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Implementations;

public class HotelsRepository : IRepository<Hotel>
{
    private readonly DatabaseContext _context;

    public HotelsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Hotel> Query => _context.Hotels;

    public async Task<bool> Add(Hotel entity)
    {
        await _context.Hotels.AddAsync(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(long entityId)
    {
        var entity = await GetByID(entityId);
        if (entity == null)
        {
            return true;
        }
        _context.Hotels.Remove(entity);
        return await SaveChanges();
    }

    public async Task<IEnumerable<Hotel>> GetAll()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<Hotel> GetByID(long entityId)
    {
        return await _context.Hotels.SingleAsync(x => x.Id == entityId);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Hotel entity)
    {
        _context.Hotels.Update(entity);
        return await SaveChanges();
    }
}
