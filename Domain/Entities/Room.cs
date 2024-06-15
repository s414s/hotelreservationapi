using Domain.Base;
using Domain.Enum;

namespace Domain.Entities;

public class Room : Entity
{
    public int Storey { get; set; }
    public RoomTypes Type { get; set; }
    public decimal? Price { get; set; }

    // Navigation properties
    public long HotelId { get; set; }
    public virtual Hotel? Hotel { get; set; }
    public ICollection<Booking>? Bookings { get; set; }

    public Room() { }
    public Room(int storey, decimal price, RoomTypes type = RoomTypes.Single)
    {
        Storey = storey;
        Price = price;
        Type = type;
    }

    public int GetCapacity() => Type == RoomTypes.Single ? 1 : 2;
}
