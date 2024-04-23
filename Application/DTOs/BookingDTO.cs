using Domain.Entities;

namespace Application.DTOs;

public class BookingDTO
{
    public long Id { get; set; }
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public string HotelName { get; set; }
    public IEnumerable<GuestDTO> Guests { get; set; } = [];

    public static BookingDTO MapFromDomainEntity(Booking booking)
    {
        return new BookingDTO
        {
            Id = booking.Id,
            From = booking.Start.ToDateTime(TimeOnly.Parse("10:00 AM")),
            Until = booking.End.ToDateTime(TimeOnly.Parse("10:00 AM")),
            Guests = booking.Guests?.Select(g => GuestDTO.MapFromDomainEntity(g)) ?? [],
            HotelName = booking.Room?.Hotel?.Name ?? string.Empty,
        };
    }

    public Booking MapToDomainEntity()
    {
        return new Booking
        {
            Start = DateOnly.FromDateTime(From),
            End = DateOnly.FromDateTime(Until),
            Guests = Guests.Select(x => x.MapToDomainEntity()).ToList(),
        };
    }
}
