using Domain.Entities;

namespace Application.DTOs;

public class GuestDTO
{
    public string Name { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;

    public static GuestDTO MapFromDomainEntity(Guest guest)
    {
        return new GuestDTO
        {
            Name = guest.Name ?? string.Empty,
            DNI = guest.DNI ?? string.Empty,
        };
    }

    public Guest MapToDomainEntity()
    {
        return new Guest(Name, DNI);
    }
}
