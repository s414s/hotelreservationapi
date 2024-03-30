using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _bookingsRepo;
    private readonly IRepository<Room> _roomsRepo;

    public BookingService(IRepository<Booking> bookingsRepo, IRepository<Room> roomsRepo)
    {
        _bookingsRepo = bookingsRepo;
        _roomsRepo = roomsRepo;
    }

    public async Task<bool> BookRoom(long roomId, IEnumerable<string> guestNames, DateOnly from, DateOnly until)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        if (!room.IsAvailableBetweenDates(from, until))
            throw new ApplicationException("the room is not available on those dates");

        var guests = guestNames.Select(x => new Guest(x, x, x)).ToList(); // TODO

        if (guests.Count == 0)
            throw new ApplicationException("no guest inserted");

        if (room.Capacity < guests.Count)
            throw new ApplicationException("the room is not big enough for the nr of guests");

        await _bookingsRepo.Add(new Booking(from, until, guests));
        return await _bookingsRepo.SaveChanges();
    }

    public async Task<bool> DeleteBooking(long bookingId)
    {
        var booking = await _bookingsRepo.GetByID(bookingId)
            ?? throw new ApplicationException("the booking does not exist");

        await _bookingsRepo.Delete(booking.Id);
        return await _bookingsRepo.SaveChanges();
    }

    public Task<IEnumerable<BookingDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookingDTO>> GetFilteredBookings(DateOnly from, DateOnly until, long? hotelId, long? clientId)
    {
        throw new NotImplementedException();
    }
}
