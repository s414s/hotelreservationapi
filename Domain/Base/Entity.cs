namespace Domain.Base;

public class Entity
{
    public long Id { get; set; }
    public bool Equal(Entity other)
    {
        return other.Id == Id;
    }
}
