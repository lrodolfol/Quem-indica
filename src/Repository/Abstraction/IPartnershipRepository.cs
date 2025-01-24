using API.Models.Dto;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IPartnershipRepository
{
    public Task<IEnumerable<PartnershipQueryDto>> SearchPartnershipAsync(
        uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );

    public Task<IEnumerable<PartnershipQueryDto>> GetByStatus(uint clientNomieesId, EPartnershipStatus status);

    public Task PostAsync(PartnershipCommandDto dto);
    public Task SetOverdueStatusAsync(uint daysOverdue);
}
