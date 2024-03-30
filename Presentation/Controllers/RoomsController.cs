using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }

    /// <summary>
    /// Provides available rooms in a range of dates
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpGet("Available")]
    public async Task<ActionResult<bool>> GetAvailableRoomsAsync([FromQuery] DateOnly startDate, DateOnly endDate, long? hotelId)
    {
        try
        {
            return Ok(await _roomService.GetAvailableRoomsOnDate(startDate, endDate, hotelId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new Room
    /// </summary>
    /// <param name="newItem"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateRoomAsync(long hotelId, [FromBody] RoomDTO newRoom)
    {
        try
        {
            
            return Ok(await _roomService.CreateRoom(newRoom, hotelId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpDelete("{roomId}")]
    public async Task<ActionResult<bool>> DeleteRoomAsync(long roomId)
    {
        try
        {
            return Ok(await _roomService.DeleteRoom(roomId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
