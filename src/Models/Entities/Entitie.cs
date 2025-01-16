namespace API.Models.Entities;

public abstract class Entitie
{
    public uint Id { get; protected set; }
    public bool Active { get; protected set; } = true;
}
