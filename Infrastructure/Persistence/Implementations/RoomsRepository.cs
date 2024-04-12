using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Implementations;

public class RoomsRepository : IRepository<Room>
{
    private readonly DatabaseContext _context;

    public RoomsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Room> Query => _context.Rooms;

    public async Task<bool> Add(Room entity)
    {
        await _context.Rooms.AddAsync(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(long entityId)
    {
        var entity = await GetByID(entityId);
        if (entity == null)
        {
            return true;
        }
        _context.Rooms.Remove(entity);
        return await SaveChanges();
    }

    public async Task<IEnumerable<Room>> GetAll()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> GetByID(long entityId)
    {
        return await _context.Rooms.SingleAsync(x => x.Id == entityId);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Room entity)
    {
        _context.Rooms.Update(entity);
        return await SaveChanges();
    }
}
