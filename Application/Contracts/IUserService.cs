using Application.DTOs;

namespace Application.Contracts;

public interface IUsersService
{
    Task<UserDTO> GetActiveUser();
    Task<UserDTO> UpdateInformation(UserDTO newUserInfo);
    Task UpdateRole(Role newRole, int targetUserId);
}
