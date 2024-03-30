using Domain.Base;

namespace Domain.Entities;

public class Guest : Entity
{
    public string Name { get; init; } = String.Empty;
    public string Address { get; init; } = String.Empty;
    public string DNI { get; init; } = String.Empty;
    public Guest() { }
    public Guest(string name, string address, string dni)
    {
        Name = name;
        Address = address;
        DNI = dni;
    }
}
