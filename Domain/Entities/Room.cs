using Domain.Base;
using Domain.Enum;

namespace Domain.Entities;

public class Room : Entity
{
    public int Storey { get; set; }
    public RoomTypes Type { get; set; }

    // Navigation properties
    public virtual Hotel? Hotel { get; set; }
    public ICollection<Booking>? Bookings { get; set; }

    public Room() { }
    public Room(int storey, RoomTypes type = RoomTypes.Single)
    {
        Storey = storey;
        Type = type;
    }

    public bool IsAvailableBetweenDates(DateOnly start, DateOnly end)
        => !Bookings.Any(x => x.Start >= start && x.End <= end);

    public int GetCapacity() => Type == RoomTypes.Single ? 1 : 2;
}
