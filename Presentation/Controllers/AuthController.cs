using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("me")]
    public ActionResult<UserDTO> GetActiveUser()
    {
        try
        {
            return Ok(_authService.GetActiveUser());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
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
            _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", $"Bearer {jwtToken}");
            return Ok(new { token = jwtToken });
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public ActionResult<bool> Logout()
    {
        try
        {
            _authService.Logout();
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
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
            return BadRequest(ex);
        }
    }

}
