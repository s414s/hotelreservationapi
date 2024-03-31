using Domain.Entities;
using Domain.Enum;

namespace Application.DTOs;

public class UserDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Roles Role { get; set; }

    public User MapToDomainEntity()
    {
        return new User
        {
            Name = Name,
            Surname = Surname,
            Role = Role,
        };
    }

    public static UserDTO MapFromDomainEntity(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Role = user.Role,
        };
    }
}
