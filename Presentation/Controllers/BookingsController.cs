using Application.Contracts;
using Application.DTOs;
using Application.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

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
    [HttpGet()]
    public async Task<ActionResult<BookingDTO>> GetFilteredBookings([FromQuery] long? hotelId, long? clientId, DateOnly? start, DateOnly? end, string? guestDNI)
    {
        try
        {
            return Ok(await _bookingService.GetFilteredBookings(start, end, hotelId, clientId, guestDNI));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Gets an specific booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [HttpGet("{bookingId}")]
    public ActionResult<BookingDTO> GetBookingById(long bookingId)
    {
        return new BookingDTO();
    }

    /// <summary>
    /// Places a booking
    /// </summary>
    /// <param name="from"></param>
    /// <param name="until"></param>
    /// <param name="guestIds"></param>
    /// <returns></returns>
    [HttpPost("{roomId}")]
    public ActionResult<BookingDTO> CreateBooking(DateOnly from, DateOnly until, IEnumerable<long> guestIds)
    {
        return new BookingDTO();
    }

    /// <summary>
    /// Deletes an specific booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [HttpDelete("{bookingId}")]
    public ActionResult<bool> DeleteBooking(long bookingId)
    {
        return true;
    }
}
