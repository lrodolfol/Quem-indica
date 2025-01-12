using API.Models.Enums;

namespace API.Models.Entities;

public class Partnerships : Entitie
{
    public Client Nominees { get; private set; } = null!;
    public Client Referrer { get; private set; } = null!;
    public EPartnershipStatus PartnershipStatus { get; private set; }
}
