namespace API.Models.Dto;

public sealed record PartnershipCommandDto(uint ClientNomieesId, uint ClientReferrerId) : BaseDto
{
    public override bool Validate()
    {
        if(ClientNomieesId == 0 || ClientReferrerId == 0) AddNotifications("Id de clientes invalidos");
        if(ClientNomieesId == ClientReferrerId) AddNotifications("Id de clientes não podem ser idênticos");

        return Notifications.Count <= 0;
    }
}

public class PartnershipQueryDto
{
    public uint ClientReferrerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FictitiousName { get; set; } = string.Empty;
    public string Segment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ValidUntil { get; set; }
}
