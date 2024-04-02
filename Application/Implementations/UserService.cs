using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Implementations;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepo;

    public UserService(IUsersRepository userRepo)
    {
        _usersRepo = userRepo;
    }

    public async Task<bool> Create(UserDTO newUser)
    {
        await _usersRepo.Add(newUser.MapToDomainEntity());
        return await _usersRepo.SaveChanges();
    }

    public async Task<bool> Delete(long userId)
    {
        await _usersRepo.Delete(userId);
        return await _usersRepo.SaveChanges();
    }
}
