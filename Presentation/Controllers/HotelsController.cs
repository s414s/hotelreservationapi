using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelsService _hotelsService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Gets filteres hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<HotelDTO>> GetHotels()
    {
        try
        {
            
            return Ok(_hotelsService.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

}
