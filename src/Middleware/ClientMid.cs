using API.Models.Dto;
using API.Models.Entities;
using API.Models.ReturnView;
using API.Models.ValueObjects;
using API.Repository.Abstraction;

namespace API.Middleware;

public class ClientMid
{
    private readonly IClientRepository _repository;
    private readonly AddressMid AddressMid;
    public ApiView apiView { get; set; }

    public ClientMid(IClientRepository repository, AddressMid addressMid)
    {
        _repository = repository;
        AddressMid = addressMid;
        apiView = new ApiView();
    }

    public async Task CreateIfIsValid(ClientDto dto)
    {
        if (!dto.Validate())
            apiView.SetValues(dto.Notifications.ToList(), 400, false);
        else
        {
            var IdAddressCreated = await AddressMid.CreateIfIsValidAndReturnLastIdAsync(dto.Address);
            if (IdAddressCreated > 0)
                await Create(dto, (uint)IdAddressCreated);
            else
                apiView.SetValues(
                    dto.Address.Notifications.ToList(),
                    dto.Address.Notifications.Count > 0 ? (ushort)400 : (ushort)500,
                    false
                );
        }
    }

    private async Task Create(ClientDto dto, uint addressId)
    {
        Client client = dto;
        client.SetAddressId(addressId);

        await _repository.PostAsync(client);
        apiView.SetCode(201);
    }
}
