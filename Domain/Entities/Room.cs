using Domain.Base;
using Domain.Enum;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Room : Entity
{
    public int Storey { get; set; }
    public RoomTypes Type { get; set; }
    //public IEnumerable<Booking> Bookings { get; set; } = [];
    [JsonIgnore]
    public int Capacity { get => Type == RoomTypes.Single ? 1 : 2; }

    public Room() { }
    public Room(int storey, RoomTypes type = RoomTypes.Single)
    {
        Storey = storey;
        Type = type;
    }

    // Access properties
    public virtual long HotelId { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }

    public bool IsAvailableBetweenDates(DateOnly start, DateOnly end)
        => !Bookings.Any(x => x.Start >= start && x.End <= end);

}
