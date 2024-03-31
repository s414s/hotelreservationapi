using Domain.Base;
using Domain.Enum;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }

    public User() { }
    public User(string name, string surname, string password, Roles role = Roles.User)
    {
        Name = name.ToLower();
        Surname = surname.ToLower();
        Password = password;
        Role = role;
    }
    public string GetUsername() => $"{Name.ToLower()}{Surname.ToLower()}";
}
