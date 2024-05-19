using Application.Contracts;
using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares;

namespace Presentation.Controllers;

[Authorize]
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
    public async Task<ActionResult<List<RoomDTO>>> GetFilteredRoomsAsync([FromQuery] DateTime startDate, DateTime endDate, long? hotelId)
    {
        try
        {
            var filters = new FiltersDTO
            {
                HotelId = hotelId,
                IsAvailable = true,
            };

            if (startDate is DateTime from && endDate is DateTime until)
            {
                filters.From = DateOnly.FromDateTime(from);
                filters.Until = DateOnly.FromDateTime(until);
            }

            return Ok(await _roomService.GetFilteredRooms(filters));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get room by id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpGet("{roomId}")]
    public async Task<ActionResult<bool>> GetRoomById(long roomId)
    {
        try
        {
            return Ok(await _roomService.GetById(roomId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [AuthorizationFilter(Roles.Admin)]
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
            return BadRequest(ex.Message);
        }
    }
}
