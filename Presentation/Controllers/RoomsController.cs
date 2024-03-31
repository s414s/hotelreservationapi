﻿using Application.Contracts;
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
    public ActionResult<bool> GetFilteredRooms([FromQuery] DateOnly startDate, DateOnly endDate, long? hotelId, bool? isAvailable)
    {
        try
        {
            return Ok(_roomService.GetFilteredRooms(startDate, endDate, hotelId, isAvailable));
        }
        catch (Exception ex)
        {
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
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
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
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

}
