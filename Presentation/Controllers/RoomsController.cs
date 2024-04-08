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
    /// Provides filtered rooms
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="hotelId">Hotel id</param>
    /// <param name="isAvailable">Is Available</param>
    /// <returns></returns>
    [HttpGet("Available")]
    public async Task<ActionResult<List<RoomDTO>>> GetFilteredRoomsAsync([FromQuery] DateTime startDate, DateTime endDate, long? hotelId, bool? isAvailable)
    {
        try
        {
            var filters = new FiltersDTO
            {
                HotelId = hotelId,
                IsAvailable = isAvailable,
                From = startDate is DateTime from ? DateOnly.FromDateTime(from) : null,
                Until = endDate is DateTime until ? DateOnly.FromDateTime(until) : null,
            };

            return Ok(await _roomService.GetFilteredRooms(filters));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Creates a new room
    /// </summary>
    /// <param name="hotelId">Hotel id</param>
    /// <param name="newRoom">New Room information</param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateRoom(long hotelId, [FromBody] RoomDTO newRoom)
    {
        try
        {
            return Ok(await _roomService.CreateRoom(newRoom, hotelId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Update a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpPut("{roomId}")]
    public async Task<ActionResult<bool>> UpdateRoom(long roomId, [FromQuery] RoomDTO updatedRoom)
    {
        try
        {
            updatedRoom.Id = roomId;
            return Ok(await _roomService.UpdateRoom(updatedRoom));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Deletes a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpDelete("{roomId}")]
    public async Task<ActionResult<bool>> DeleteRoom(long roomId)
    {
        try
        {
            return Ok(await _roomService.DeleteRoom(roomId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
}
