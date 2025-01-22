using API.Models.ValueObjects;

namespace API.Models.Dto;

public sealed record AddressDto : BaseDto
{
    public string Country { get; set; } = null!;
    public string State { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Additional { get; set; } = null!;
    public ushort Number { get; set; }

    public override bool Validate()
    {
        if (string.IsNullOrWhiteSpace(Country)) AddNotifications("Nome não pode ser vazio");
        if (string.IsNullOrWhiteSpace(State)) AddNotifications("Apelido não pode ser vazio");
        if (string.IsNullOrWhiteSpace(City)) AddNotifications("Apelido não pode ser vazio");
        if (string.IsNullOrWhiteSpace(ZipCode)) AddNotifications("Apelido não pode ser vazio");
        if (string.IsNullOrWhiteSpace(District)) AddNotifications("Apelido não pode ser vazio");
        if (string.IsNullOrWhiteSpace(Street)) AddNotifications("Apelido não pode ser vazio");
        if (Number <= 0) AddNotifications("Numero deve ser válido");

        return Notifications.Count <= 0;
    }

    public static implicit operator Address(AddressDto dto) =>
        new Address(dto.Country, dto.State, dto.City, dto.ZipCode, dto.District, dto.Street, dto.Additional, dto.Number);
}