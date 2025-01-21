using API.Models.Enums;

namespace API.Models.Entities;

public class Partnerships : Entitie
{
    public List<Client> Nominees { get; set; } = [];
    public List<Client> Referrer { get; set; } = [];
    public EPartnershipStatus PartnershipStatus { get; private set; }
}
