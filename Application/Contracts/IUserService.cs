using Application.DTOs;

namespace Application.Contracts;

public interface IUserService
{
    Task<UserDTO> UpdateInformation(UserDTO newUserInfo);
    Task Create(UserDTO newUser);
    Task Delete(UserDTO user);
}
