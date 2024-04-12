using Application.Contracts;
using Application.DTOs;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <summary>
    /// Gets active user info
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [AuthorizationFilter(Roles.Admin)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDTO>> GetActiveUserAsync()
    {
        try
        {
            return Ok(await _authService.GetActiveUser());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<object>> LoginAsync([FromBody] LoginDTO loginInfo)
    {
        try
        {
            var jwtToken = await _authService.Login(loginInfo);
            _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", $"bearer {jwtToken}");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", jwtToken, new CookieOptions { HttpOnly = true });
            return Ok(new { token = jwtToken });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            await _authService.Logout();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    /// <summary>
    /// Sign up
    /// </summary>
    /// <param name="signupInfo"></param>
    /// <returns></returns>
    [HttpPost("signup")]
    public ActionResult<bool> Signup([FromBody] SignupDTO signupInfo)
    {
        try
        {
            _authService.SignUp(signupInfo);
            return Ok(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

}
