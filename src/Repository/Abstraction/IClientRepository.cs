using API.Models.Entities;
using API.Models.Enums;

namespace API.Repository.Abstraction;

public interface IClientRepository
{
    public IEnumerable<Partnerships> SearchPartnershipByClient(
        int clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED
    );
    public Task<Client> GetAsync(int id);
    public Task PostAsync(Client entity);
    public Task PutAsync(int id, Client entity);
    public Task DeleteAsync(int id);
}
