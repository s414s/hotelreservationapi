using Application.DTOs;

namespace Application.Contracts;

public interface IBookingService
{
    Task<IEnumerable<BookingDTO>> GetAll();
    Task<bool> BookRoom(Guid roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until);
    Task<bool> DeleteBooking(Guid bookingId);
    Task<IEnumerable<BookingDTO>> GetBookingsOfMonth(int month, int year, Guid? hotelId);
}
