using API.Models.Entities;

namespace API.Models.ValueObjects;

public class Address : Entitie
{
    public string? Country { get; private set; }
    public string? State { get; private set; }
    public string? City { get; private set; }
    public string? ZipCode { get; private set; }
    public string? District { get; private set; }
    public string? Street { get; private set; }
    public string? Additional { get; private set; }
    public int Number { get; private set; }

    public Address(string? country, string? state, string? city, string? zipCode, string? district, string? street, string? additional, int number)
    {
        Country = country;
        State = state;
        City = city;
        ZipCode = zipCode;
        District = district;
        Street = street;
        Additional = additional;
        Number = number;
    }
}
