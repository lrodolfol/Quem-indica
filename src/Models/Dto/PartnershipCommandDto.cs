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

public record PartnershipQueryDto(
    uint ClientReferrerId, 
    string Name,  
    string FictitiousName, 
    string Segment,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateOnly ValidUntil
    );
