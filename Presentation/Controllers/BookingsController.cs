﻿using Application.Contracts;
using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    /// <summary>
    /// Gets filtered bookings
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<ActionResult<List<BookingDTO>>> GetFilteredBookings([FromQuery] long? hotelId, long? clientId, DateTime? start, DateTime? end, string? guestDNI, bool? asc, string? fieldsToOrderBy)
    {
        try
        {
            var filters = new FiltersDTO
            {
                HotelId = hotelId,
                ClientId = clientId,
                From = start is DateTime from ? DateOnly.FromDateTime(from) : null,
                Until = end is DateTime until ? DateOnly.FromDateTime(until) : null,
                GuestDNI = guestDNI,
                Asc = asc ?? true,
                FieldToOrderBy = fieldsToOrderBy ?? string.Empty,
            };

            return Ok(await _bookingService.GetFilteredBookings(filters));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Places a booking
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPost("{roomId}")]
    public async Task<ActionResult<bool>> CreateBooking(long roomId, [FromBody] BookingDTO booking)
    {
        try
        {
            return Ok(await _bookingService.BookRoom(roomId, booking));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes an specific booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [AuthorizationFilter(Roles.Admin)]
    [HttpDelete("{bookingId}")]
    public async Task<ActionResult<bool>> DeleteBooking(long bookingId)
    {
        try
        {
            return Ok(await _bookingService.DeleteBooking(bookingId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
