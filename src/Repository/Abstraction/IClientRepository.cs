using API.Models.Entities;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IClientRepository
{
    public IEnumerable<Partnerships> SearchPartnershipByClient(
        uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );
    public Task<Client> GetAsync(uint id);
    public Task<IEnumerable<Client>> GetAsync(uint limit, uint offset);
    public Task PostAsync(Client entity);
    public Task PutAsync(uint id, Client entity);
    public Task DeleteAsync(uint id);
}
