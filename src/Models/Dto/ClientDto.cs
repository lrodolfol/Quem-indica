using API.Models.Enums;

namespace API.Models.Dto;

public sealed class ClientDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string FictitiousName { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
    public ESegment Segment { get; set; }

    public bool Validate()
    {
        if(string.IsNullOrWhiteSpace(Name)) AddNotifications("Nome não pode ser vazio");
        if(string.IsNullOrWhiteSpace(FictitiousName)) AddNotifications("Apelido não pode ser vazio");

        return Notifications.Count <= 0;
    }
}
