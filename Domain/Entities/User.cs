using Domain.Base;
using Domain.Enum;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public string Username { get => $"{Name.ToLower()}{Surname.ToLower()}"; }

    public User() { }
    public User(string name, string surname, string password, Roles role = Roles.User)
    {
        Name = name;
        Surname = surname;
        Password = password;
        Role = role;
    }
}
