using API.Models.Enums;
using API.Models.ValueObjects;

namespace API.Models.Entities;

public class Client : Entitie
{
    public string Name { get; set; } = null!;
    public string FictitiousName { get; private set; } = null!;
    public Address? Address { get; private set; }
    public ESegment Segment { get; set; } = ESegment.Desconhecido;

    public Client(string name, string fictitiousName, Address? address, ESegment segment)
    {
        Name = name;
        FictitiousName = fictitiousName;
        Address = address;
        Segment = segment;
    }
}
