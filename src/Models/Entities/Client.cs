using API.Models.Dto;
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

    public static implicit operator Client (ClientDto dto)
    {
        var address = new Address(dto.Address.Country, dto.Address.State, dto.Address.City, dto.Address.ZipCode, 
            dto.Address.District, dto.Address.Street, dto.Address.Additional, dto.Address.Number);

        return new Client(dto.Name, dto.FictitiousName, address, dto.Segment);
    }
}
