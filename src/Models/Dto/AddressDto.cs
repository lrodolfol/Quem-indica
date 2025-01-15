namespace API.Models.Dto;

public class AddressDto
{
    public string Country { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string ZipCode { get; private set; } = null!;
    public string District { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string Additional { get; private set; } = null!;
    public int Number { get; private set; }
}