using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Implementations;

public class BookingsRepository : IRepository<Booking>
{
    private readonly DatabaseContext _context;

    public BookingsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<Booking> Query => _context.Bookings;

    public async Task<bool> Add(Booking entity)
    {
        await _context.Bookings.AddAsync(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(long entityId)
    {
        var entity = await GetByID(entityId);
        if (entity == null)
        {
            return true;
        }
        _context.Bookings.Remove(entity);
        return await SaveChanges();
    }

    public async Task<IEnumerable<Booking>> GetAll()
    {
        return await _context.Bookings.ToListAsync();
    }

    public async Task<Booking> GetByID(long entityId)
    {
        return await _context.Bookings.SingleAsync(x => x.Id == entityId);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Booking entity)
    {
        _context.Bookings.Update(entity);
        return await SaveChanges();
    }
}
