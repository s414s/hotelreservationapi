using Application.Contracts;
using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    //[Authorize(Roles = "User")]
    //[Authorize]
    [HttpGet()]
    public ActionResult<IEnumerable<HotelDTO>> GetHotels([FromQuery] Cities? city)
    {
        try
        {
            return Ok(_hotelsService.GetFilteredHotels(city));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Gets hotel rooms
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpGet("{hotelId}/Rooms")]
    public async Task<ActionResult<HotelDTO>> GetHotelWithRooms(long hotelId)
    {
        try
        {
            return Ok(await _hotelsService.GetById(hotelId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Gets hotel by id
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{hotelId}")]
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotelsAsync(long hotelId)
    {
        try
        {
            return Ok(await _hotelsService.GetById(hotelId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Creates a new hotel
    /// </summary>
    /// <param name="hotelInfo"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateHotel([FromBody] HotelDTO hotelInfo)
    {
        try
        {
            return Ok(await _hotelsService.Create(hotelInfo));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}
