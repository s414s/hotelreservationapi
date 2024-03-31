using Domain.Base;

namespace Domain.Entities;

public class Guest : Entity
{
    public string Name { get; set; }
    public string DNI { get; set; }

    // Navigation properties
    public long BookingId { get; set; }
    public Booking Booking { get; set; }

    public Guest() { }
    public Guest(string name, string dni)
    {
        Name = name.ToLower();
        DNI = dni;
    }
}
