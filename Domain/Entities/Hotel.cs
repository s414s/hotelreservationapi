using Domain.Base;
using Domain.DomainServices;
using Domain.Enum;

namespace Domain.Entities;

public class Hotel : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Cities City { get; set; }

    // Navigation properties
    public ICollection<Room>? Rooms { get; set; }

    public Hotel() { }
    public Hotel(string name, string address, Cities city)
    {
        Name = name.ToLower();
        Address = address.ToLower();
        City = city;
    }

    public IEnumerable<Room> GetAvailableRoomsBetweenDates(DateOnly start, DateOnly end)
        => Rooms.Where(x => AvailabilityService.IsRoomAvailableBetweenDates(x, start, end));

}
