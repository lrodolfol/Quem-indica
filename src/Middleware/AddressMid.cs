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

    public async Task<int> CreateIfIsValidAndReturnLastIdAsync(AddressDto dto)
    {
        if (!dto.Validate())
            return 0;
        else
            return await CreateAndReturnLastIdAsync(dto);
    }

    private async Task<int> CreateAndReturnLastIdAsync(AddressDto dto)
    {
        Address address = dto;
        return await _repository.PostReturnLastIdAsync(address);
    }
}
