using Domain.Base;
using Domain.Enum;

namespace Domain.Entities;

public class Hotel : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
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
        => Rooms.Where(x => x.IsAvailableBetweenDates(start, end));

    public int GetOccupationRatioOnDate(DateOnly date)
        => Rooms.Where(x => !x.IsAvailableBetweenDates(date, date)).Count() / Rooms.Count();
}

