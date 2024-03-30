using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomsService _roomsService;
    private readonly ILogger<RoomsController> _logger;

    /// <summary>
    /// Provides available rooms in a range of dates
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpGet("Available")]
    public ActionResult<bool> GetAvailableItems([FromQuery] DateTime startDate, DateTime endDate, Guid hotelId)
    {
        return true;
    }

    /// <summary>
    /// Creates a new Room
    /// </summary>
    /// <param name="newItem"></param>
    /// <returns></returns>
    [HttpPost()]
    public ActionResult<bool> CreateItem(int modelId, [FromBody] RoomDTO newRoom)
    {
        try
        {
            _roomsService.CreateItem(newRoom, modelId);
            return Ok(true);
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
    public ActionResult<bool> CreateItem(long roomId)
    {
        try
        {
            _roomsService.DeleteItem(roomId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
