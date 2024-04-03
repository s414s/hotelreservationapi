using Domain.Entities;

namespace Application.DTOs;

public class BookingDTO
{
    public long Id { get; set; }
    public DateOnly From { get; set; }
    public DateOnly Until { get; set; }
    public IEnumerable<GuestDTO> Guests { get; set; } = [];
    public static BookingDTO MapFromDomainEntity(Booking booking)
    {
        return new BookingDTO
        {
            Id = booking.Id,
            From = booking.Start,
            Until = booking.End,
            Guests = booking.Guests?.Select(g => GuestDTO.MapFromDomainEntity(g)) ?? [],
        };
    }

    public Booking MapToDomainEntity()
    {
        return new Booking
        {
            Start = From,
            End = Until,
            Guests = Guests.Select(x => x.MapToDomainEntity()).ToList(),
        };
    }
}
