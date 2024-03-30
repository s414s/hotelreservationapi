using Application.DTOs;

namespace Application.Contracts;

public interface IUserService
{
    Task<bool> Create(UserDTO newUser);
    Task<bool> Delete(long userId);
}
