using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Implementations;

public class UserService : IUserService
{
    private readonly IRepository<User> _usersRepo;

    public UserService(IRepository<User> userRepo)
    {
        _usersRepo = userRepo;
    }

    public async Task CreateAsync(UserDTO newUser)
    {
        await _usersRepo.Add(newUser.MapToDomainEntity());
        await _usersRepo.SaveChanges();
    }

    public async Task DeleteAsync(UserDTO user)
    {
        await _usersRepo.Delete(user.MapToDomainEntity());
        await _usersRepo.SaveChanges();
    }

    public async Task<UserDTO?> SignInAsync(string username, string password)
    {
        var user = await _usersRepo
            .GetAll()
            .FirstOrDefault(x => x.Username == username);

        return user?.Password != password
            ? null
            : UserDTO.MapFromDomainEntity(user);
    }
}
