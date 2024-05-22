using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; } = string.Empty;
}
