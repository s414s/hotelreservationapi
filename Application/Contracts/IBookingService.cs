using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    Task<bool> BookRoom(long roomId, IEnumerable<GuestDTO> guests, DateOnly from, DateOnly until);
    Task<bool> DeleteBooking(long bookingId);
    Task<IEnumerable<BookingDTO>> GetFilteredBookings(DateOnly? from, DateOnly? until, long? hotelId, long? roomId, string? guestDNI);
}
