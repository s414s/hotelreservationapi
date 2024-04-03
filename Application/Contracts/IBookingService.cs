using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    Task<bool> BookRoom(long roomId, BookingDTO booking);
    Task<bool> DeleteBooking(long bookingId);
    Task<IEnumerable<BookingDTO>> GetFilteredBookings(FiltersDTO filters);
}
