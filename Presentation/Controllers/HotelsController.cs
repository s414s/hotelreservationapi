using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
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
    /// Gets filteres hotels with their rooms
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels([FromQuery] Cities? city, bool? asc)
    {
        try
        {
            var filter = new FiltersDTO { Asc = asc ?? true, City = city, };
            return Ok(await _hotelsService.GetFilteredHotels(filter));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets hotel rooms
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets hotel by id
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("{hotelId}")]
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels(long hotelId)
    {
        try
        {
            return Ok(await _hotelsService.GetById(hotelId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new hotel
    /// </summary>
    /// <param name="newHotelInfo"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateHotel([FromBody] NewHotelDTO newHotelInfo)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _hotelsService.Create(newHotelInfo));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
