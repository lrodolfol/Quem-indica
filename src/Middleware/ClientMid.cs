using API.Models.Dto;
using API.Models.Entities;
using API.Models.ReturnView;
using API.Repository.Abstraction;
using System.Net;

namespace API.Middleware;

public class ClientMid
{
    private readonly IClientRepository _repository;
    private readonly AddressMid AddressMid;
    public ApiView ApiView { get; private set; }

    public ClientMid(IClientRepository repository, AddressMid addressMid)
    {
        _repository = repository;
        AddressMid = addressMid;
        ApiView = new ApiView();
    }

    public async Task CreateIfIsValid(ClientDto dto)
    {
        try
        {
            if (!dto.Validate())
                ApiView.SetValues(dto.Notifications.ToList(), HttpStatusCode.BadRequest, false);
            else
            {
                var IdAddressCreated = await AddressMid.CreateIfIsValidAndReturnLastIdAsync(dto.Address);
                if (IdAddressCreated > 0)
                    await Create(dto, (uint)IdAddressCreated);
                else
                    ApiView.SetValues(
                        dto.Address.Notifications.ToList(),
                        dto.Address.Notifications.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError,
                        false
                    );
            }
        }
        catch
        {
            ApiView.SetValues("Falha interna no cadastro", HttpStatusCode.InternalServerError, false);
        }
    }

    private async Task Create(ClientDto dto, uint addressId)
    {
        Client client = dto;
        client.SetAddressId(addressId);

        await _repository.PostAsync(client);
        ApiView.SetCode(HttpStatusCode.Created);
    }

    public async Task UpdateIfIsValid(uint id, ClientDto dto)
    {
        try
        {
            if(dto.Validate())
                await UpdateAsync(id, dto);
            else
                ApiView.SetValues("Dados inválidos", HttpStatusCode.BadRequest, false);
        }
        catch
        {
            ApiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
        }        
    }

    private async Task UpdateAsync(uint id, ClientDto dto)
    {
        Client client = await _repository.GetAsync(id);

        if (client is null)
        {
            ApiView.SetValues("Id de client não encontrado", HttpStatusCode.NotFound, false);
            return;
        }

        await _repository.PutAsync(id, dto);
        ApiView.SetCode(HttpStatusCode.NoContent);
    }

    public async Task GetAsync(uint id)
    {
        Client client = await _repository.GetAsync(id);

        if (client is null)
            ApiView.SetValues("Id de client não encontrado", HttpStatusCode.NotFound, false);
        else
            ApiView.SetData(client);
    }

    public async Task GetAsync(uint limit, uint offetPageNumber)
    {
        IEnumerable<Client>? client = await _repository.GetAsync(limit, offetPageNumber);

        ApiView.SetData(client);
    }

    public async Task DeleteAsync(uint id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch
        {
            ApiView.SetValues("Falha no delete da entidade", HttpStatusCode.InternalServerError, false);
        }
    }
}
