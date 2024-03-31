using Domain.Base;
using Domain.Enum;

namespace Domain.Entities;

public class User : Entity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Password { get; set; }
    public Roles Role { get; set; }

    public User() { }
    public User(string name, string surname, string password, Roles role = Roles.User)
    {
        Name = name;
        Surname = surname;
        Password = password;
        Role = role;
    }
    public string GetUsername() => $"{Name.ToLower()}{Surname.ToLower()}";
}
