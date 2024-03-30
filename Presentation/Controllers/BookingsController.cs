using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingsService _ordersService;
    private readonly ILogger<BookingsController> _logger;

    /// <summary>
    /// Gets filtered bookings
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<BookingDTO> GetFilteredBookings([FromQuery] long hotelId, DateOnly start, DateOnly end)
    {
        return new BookingDTO();
    }

    /// <summary>
    /// Gets an specific booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [HttpGet("{orderId}")]
    public ActionResult<BookingDTO> GetBookingById(long bookingId)
    {
        return new BookingDTO();
    }

    /// <summary>
    /// Gets all bookings associated to a client
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpGet("client/{clientId}")]
    public ActionResult<BookingsByClientDTO> GetOrdersByClient(long clientId)
    {
        return new BookingsByClientDTO();
    }

    /// <summary>
    /// Deletes an specific booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    [HttpDelete("{bookingId}")]
    public ActionResult<bool> DeleteOrderById(long bookingId)
    {
        return true;
    }
}
