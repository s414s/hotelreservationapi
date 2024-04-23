using Domain.Entities;
using Domain.Enum;

namespace Application.DTOs;

public class HotelDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Cities City { get; set; }
    public int NumberOfRooms { get; set; }
    public List<RoomDTO> Rooms { get; set; } = [];

    public static HotelDTO MapFromDomainEntity(Hotel hotel)
    {
        return new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            City = hotel.City,
            NumberOfRooms = hotel.Rooms?.Count ?? 0,
            Rooms = hotel.Rooms?.Select(r => RoomDTO.MapFromDomainEntity(r)).ToList() ?? [],
        };
    }

    public Hotel MapToDomainEntity() => new(Name, Address, City);
}
