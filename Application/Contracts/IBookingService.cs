using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts;

public interface IBookingService
{
    Task<bool> BookRoom(long roomId, BookingDTO booking);
    Task<bool> DeleteBooking(long bookingId);
    Task<IEnumerable<BookingDTO>> GetFilteredBookings(FiltersDTO filters);
}
