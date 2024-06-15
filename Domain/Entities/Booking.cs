using Domain.Base;

namespace Domain.Entities;

public class Booking : Entity
{
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation properties
    public long RoomId { get; set; }
    public virtual Room? Room { get; set; }
    public ICollection<Guest>? Guests { get; set; }

    public Booking() { }
    public Booking(DateOnly start, DateOnly end, List<Guest> guests)
    {
        Start = start;
        End = end;
        Guests = guests;
    }
}
