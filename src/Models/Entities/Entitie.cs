namespace API.Models.Entities;

public abstract class Entitie
{
    public Guid Id { get; private set; }
    public bool Active { get; private set; } = true;
}
