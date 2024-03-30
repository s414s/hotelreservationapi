using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    Task<IEnumerable<BookingDTO>> GetAll();
    Task<bool> BookRoom(long roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until);
    Task<bool> DeleteBooking(long bookingId);
    Task<IEnumerable<BookingDTO>> GetFilteredBookings(DateOnly from, DateOnly until, long? hotelId, long? clientId);
}
