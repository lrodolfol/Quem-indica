using API.Models.Dto;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IPartnershipRepository
{
    public Task<IEnumerable<PartnershipQueryDto>> SearchPartnershipIAmNomiessAsync(
        uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );
    public Task<IEnumerable<PartnershipQueryDto>> GetByStatusAsync(uint clientNomieesId, EPartnershipStatus status);
    //public Task<Partnerships> GetByIdAsync(uint partnershipId);
    public Task PostAsync(PartnershipCommandDto dto);
    public Task SetOverdueStatusAsync(uint daysOverdue);
    public Task AcceptRequestPartnershipAsync(uint clientNomieesId, uint partnershipId);
}
