using Domain.Base;

namespace Domain.Entities;

public class Guest : Entity
{
    public required string Name { get; set; }
    public required string DNI { get; set; }

    // Navigation properties
    public virtual ICollection<Booking>? Bookings { get; set; }
    public Guest() { }
    public Guest(string name, string dni)
    {
        Name = name;
        DNI = dni;
    }
}
