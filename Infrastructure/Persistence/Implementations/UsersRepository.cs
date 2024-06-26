﻿using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Implementations;

public class UsersRepository : IUsersRepository
{
    private readonly DatabaseContext _context;

    public UsersRepository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<User> Query => _context.Users;

    public async Task<bool> Add(User entity)
    {
        await _context.Users.AddAsync(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(long entityId)
    {
        var entity = await GetByID(entityId);
        if (entity == null)
        {
            return true;
        }
        _context.Users.Remove(entity);
        return await SaveChanges();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByCredentials(string username, string password)
    {
        return await _context.Users
            .SingleOrDefaultAsync(x => x.Name + x.Surname == username && x.Password == password);
    }

    public async Task<User> GetByID(long entityId)
    {
        return await _context.Users.SingleAsync(x => x.Id == entityId);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(User entity)
    {
        _context.Users.Update(entity);
        return await SaveChanges();
    }
}
