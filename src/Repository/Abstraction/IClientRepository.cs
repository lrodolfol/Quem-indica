using API.Models.Entities;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IClientRepository
{
    public IEnumerable<Partnerships> SearchPartnershipByClient(
        int clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );
}
