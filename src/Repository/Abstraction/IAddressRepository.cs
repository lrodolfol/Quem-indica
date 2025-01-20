using API.Models.ValueObjects;

namespace API.Repository.Abstraction;

public interface IAddressRepository
{
    public Task<Address> GetAsync(int id);
    public Task<int> PostReturnLastIdAsync(Address entity);
    public Task PutAsync(int id, Address entity);
    public Task DeleteAsync(int id);
}
