using Domain.Entities;
using Domain.Enum;

namespace Application.DTOs;

public class HotelDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Cities City { get; set; }
    public int NumberOfRooms { get; set; }
    public IEnumerable<RoomDTO> Rooms { get; set; } = Enumerable.Empty<RoomDTO>();
    public static HotelDTO MapFromDomainEntity(Hotel hotel)
    {
        return new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            City = hotel.City,
            NumberOfRooms = hotel.Rooms.Count(),
            Rooms = hotel.Rooms.Select(r => RoomDTO.MapFromDomainEntity(r)),
        };
    }
}
