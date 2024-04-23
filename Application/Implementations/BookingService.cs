using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _bookingsRepo;
    private readonly IRepository<Room> _roomsRepo;

    public BookingService(
        IRepository<Booking> bookingsRepo,
        IRepository<Room> roomsRepo)
    {
        _bookingsRepo = bookingsRepo;
        _roomsRepo = roomsRepo;
    }

    public async Task<bool> BookRoom(long roomId, BookingDTO booking)
    {
        var room = await _roomsRepo.GetByID(roomId)
            ?? throw new ApplicationException("the room does not exist");

        if (!room.IsAvailableBetweenDates(DateOnly.FromDateTime(booking.From), DateOnly.FromDateTime(booking.Until)))
            throw new ApplicationException("the room is not available on those dates");

        if (room.GetCapacity() < booking.Guests.Count())
            throw new ApplicationException("the room is not big enough for the number of guests");

        var newBooking = booking.MapToDomainEntity();
        newBooking.RoomId = roomId;

        await _bookingsRepo.Add(newBooking);
        return await _bookingsRepo.SaveChanges();
    }

    public async Task<bool> DeleteBooking(long bookingId)
    {
        var booking = await _bookingsRepo.GetByID(bookingId)
            ?? throw new ApplicationException("the booking does not exist");

        await _bookingsRepo.Delete(booking.Id);
        return await _bookingsRepo.SaveChanges();
    }

    public async Task<IEnumerable<BookingDTO>> GetFilteredBookings(FiltersDTO filters)
    {
        return await _bookingsRepo.Query
            .Include(b => b.Guests)
            .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
            .Where(b => filters.GuestDNI == null || b.Guests.Any(x => x.DNI == filters.GuestDNI))
            .Where(b => filters.HotelId == null || b.Room.HotelId == filters.HotelId)
            .Where(b => filters.RoomId == null || b.RoomId == filters.RoomId)
            .Where(b => filters.From == null || b.Start >= filters.From)
            .Where(b => filters.Until == null || b.End >= filters.Until)
            .Select(b => BookingDTO.MapFromDomainEntity(b))
            .ToListAsync();
    }
}
