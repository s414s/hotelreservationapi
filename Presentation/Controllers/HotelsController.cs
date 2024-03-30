﻿using Application.Contracts;
using Application.DTOs;
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
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotelsAsync()
    {
        try
        {
            return Ok(await _hotelsService.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

}
