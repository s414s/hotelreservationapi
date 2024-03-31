using Application.Contracts;
using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelsService;
    private readonly ILogger<AuthController> _logger;

    public HotelsController(IHotelService hotelsService, ILogger<AuthController> logger)
    {
        _hotelsService = hotelsService;
        _logger = logger;
    }

    /// <summary>
    /// Gets filteres hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<HotelDTO>> GetHotels([FromQuery] Cities? city)
    {
        try
        {
            return Ok(_hotelsService.GetFilteredHotels(city));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

}
