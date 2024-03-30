namespace Application.DTOs;

public class SignupDTO
{
    public string Username { get; init; } = String.Empty;
    public string Name { get; init; } = String.Empty;
    public string Surname { get; init; } = String.Empty;
    public string Password { get; init; } = String.Empty;
    public string RepeatPassword { get; init; } = String.Empty;    
    public string DNI { get; init; } = String.Empty;
}
