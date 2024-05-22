using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class SignupDTO
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Surname is required.")]
    public string Surname { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; } = string.Empty;
}
