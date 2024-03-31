﻿using Domain.Entities;

namespace Application.DTOs;

public class BookingDTO
{
    public long Id { get; set; }
    public DateOnly From { get; set; }
    public DateOnly Until { get; set; }
    public IEnumerable<string> GuestNames { get; set; } = [];
    public string HotelName { get; set; } = string.Empty;
    public static BookingDTO MapFromDomainEntity(Booking booking)
    {
        return new BookingDTO
        {
            Id = booking.Id,
            From = booking.Start,
            Until = booking.End,
            GuestNames = booking.Guests.Select(g => g.Name),
        };
    }
}
