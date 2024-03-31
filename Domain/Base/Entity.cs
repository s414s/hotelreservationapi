using System.ComponentModel.DataAnnotations;

namespace Domain.Base;

public class Entity
{
    [Key]
    public long Id { get; set; }
    public bool Equal(Entity other)
    {
        return other.Id == Id;
    }
}
