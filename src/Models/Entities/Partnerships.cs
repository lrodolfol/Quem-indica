using API.Models.Enums;

namespace API.Models.Entities;

public class Partnerships : Entitie
{
    public List<Client> Nominees { get; set; } = [];
    public List<Client> Referrer { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateOnly ValidUntil { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
    public EPartnershipStatus PartnershipStatus { get; private set; }
}
