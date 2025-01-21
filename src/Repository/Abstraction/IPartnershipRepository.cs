using API.Models.Dto;
using API.Models.Entities;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IPartnershipRepository
{
    public Task<IEnumerable<PartnershipQueryDto>> SearchPartnershipByClient(
        uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );

    public Task PostAsync(PartnershipCommandDto dto);
}
