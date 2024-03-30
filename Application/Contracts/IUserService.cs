using Application.DTOs;

namespace Application.Contracts;

public interface IUsersService
{
    Task<UserDTO> UpdateInformation(UserDTO newUserInfo);
    Task Create(UserDTO newUser);
    Task Delete(UserDTO user);
}
