﻿using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.DomainServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations;

public class BookingService(IRepository<Booking> bookingsRepo, IRepository<Room> roomsRepo) : IBookingService
{
    private readonly IRepository<Booking> _bookingsRepo = bookingsRepo;
    private readonly IRepository<Room> _roomsRepo = roomsRepo;

    public async Task<bool> BookRoom(long roomId, BookingDTO booking)
    {
        var room = await _roomsRepo.Query
            .Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == roomId)
            ?? throw new ApplicationException("the room does not exist");

        if (!AvailabilityService.IsRoomAvailableBetweenDates(room, DateOnly.FromDateTime(booking.From), DateOnly.FromDateTime(booking.Until)))
            throw new ApplicationException("the room is not available on those dates");

        if (room.GetCapacity() < booking.Guests.Count)
            throw new ApplicationException("the room is not big enough for the number of guests");

        var newBooking = booking.MapToDomainEntity();
        newBooking.Room = room;
        newBooking.TotalPrice = PriceService.CalculateTotalBookingPrice(newBooking);

        return await _bookingsRepo.Add(newBooking);
    }

    public async Task<bool> DeleteBooking(long bookingId)
    {
        var booking = await _bookingsRepo.GetByID(bookingId)
            ?? throw new ApplicationException("the booking does not exist");

        return await _bookingsRepo.Delete(booking.Id);
    }

    public async Task<IEnumerable<BookingDTO>> GetFilteredBookings(FiltersDTO filters)
    {
        var sortProperty = filters.FieldToOrderBy == string.Empty ? "HotelName" : filters.FieldToOrderBy;

        var prop = typeof(BookingDTO).GetProperty(sortProperty)
            ?? throw new ApplicationException($"field to order by not allowed, it must be one of the following: {string.Join(", ", typeof(BookingDTO).GetProperties().Select(x => x.Name))}");

        var query = await _bookingsRepo.Query
            .Include(b => b.Guests)
            .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
            .Where(b => filters.GuestDNI == null || b.Guests.Any(x => x.DNI == filters.GuestDNI))
            .Where(b => filters.HotelId == null || b.Room.HotelId == filters.HotelId)
            .Where(b => filters.RoomId == null || b.RoomId == filters.RoomId)
            .Where(b => filters.From == null || b.Start >= filters.From)
            .Where(b => filters.Until == null || b.End <= filters.Until)
            .Select(b => BookingDTO.MapFromDomainEntity(b))
            .ToListAsync();

        return filters.Asc
            ? query.OrderBy(x => prop.GetValue(x, null))
            : query.OrderByDescending(x => prop.GetValue(x, null));
    }
}
