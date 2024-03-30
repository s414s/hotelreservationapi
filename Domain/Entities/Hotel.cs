using Domain.Base;
using System.Net;

namespace Domain.Entities;

public class Hotel : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public IEnumerable<Room> Rooms { get; set; } = [];
    public IEnumerable<User> Users { get; set; } = [];

    public Hotel() { }
    public Hotel(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public IEnumerable<Room> GetAvailableRoomsBetweenDates(DateOnly start, DateOnly end)
        => Rooms.Where(x => x.IsAvailableBetweenDates(start, end));

    public int GetOccupationRatioOnDate(DateOnly date)
        => Rooms.Where(x => !x.IsAvailableBetweenDates(date, date)).Count() / Rooms.Count();
}

