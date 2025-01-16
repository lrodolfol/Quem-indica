using API.Models.Dto;
using API.Models.ValueObjects;
using API.Repository.Abstraction;

namespace API.Middleware;

public class AddressMid
{
    private readonly IAddressRepository _repository;

    public AddressMid(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateIfIsValid(AddressDto dto)
    {
        if (!dto.Validate())
            return false;
        else
            return await Create(dto);
    }

    private async Task<bool> Create(AddressDto dto)
    {
        Address address = dto;
        await _repository.PostAsync(address);

        return true;
    }
}
