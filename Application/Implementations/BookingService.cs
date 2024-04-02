using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Hotel> _hotelsRepo;
    private readonly IRepository<Booking> _bookingsRepo;
    private readonly IRepository<Room> _roomsRepo;

    public BookingService(
        IRepository<Hotel> hotelsRepo,
        IRepository<Booking> bookingsRepo,
        IRepository<Room> roomsRepo)
    {
        _hotelsRepo = hotelsRepo;
        _bookingsRepo = bookingsRepo;
        _roomsRepo = roomsRepo;
    }

    public async Task<bool> BookRoom(long roomId, IEnumerable<GuestDTO> guests, DateOnly from, DateOnly until)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        if (!room.IsAvailableBetweenDates(from, until))
            throw new ApplicationException("the room is not available on those dates");

        if (room.GetCapacity() < guests.Count())
            throw new ApplicationException("the room is not big enough for the number of guests");

        await _bookingsRepo.Add(new Booking(from, until, guests.Select(x => x.MapToDomainEntity()).ToList()));
        return await _bookingsRepo.SaveChanges();
    }

    public async Task<bool> DeleteBooking(long bookingId)
    {
        var booking = await _bookingsRepo.GetByID(bookingId)
            ?? throw new ApplicationException("the booking does not exist");

        await _bookingsRepo.Delete(booking.Id);
        return await _bookingsRepo.SaveChanges();
    }

    public async Task<IEnumerable<BookingDTO>> GetFilteredBookings(DateOnly? from, DateOnly? until, long? hotelId, long? roomId, string? guestDNI)
    {
        return await _bookingsRepo.Query
            .Include(b => b.Guests)
            .Include(b => b.Room)
            .Where(b => guestDNI == null || b.Guests.Any(x => x.DNI == guestDNI))
            .Where(b => hotelId == null || b.Room.HotelId == hotelId)
            .Where(b => roomId == null || b.RoomId == roomId)
            .Where(b => from == null || b.Start >= from)
            .Where(b => until == null || b.End >= until)
            .Select(x => BookingDTO.MapFromDomainEntity(x))
            .ToListAsync();
    }
}
