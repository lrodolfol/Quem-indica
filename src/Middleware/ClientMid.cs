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
    public ApiView apiView { get; set; }

    public ClientMid(IClientRepository repository, AddressMid addressMid)
    {
        _repository = repository;
        AddressMid = addressMid;
        apiView = new ApiView();
    }

    public async Task CreateIfIsValid(ClientDto dto)
    {
        try
        {
            if (!dto.Validate())
                apiView.SetValues(dto.Notifications.ToList(), HttpStatusCode.BadRequest, false);
            else
            {
                var IdAddressCreated = await AddressMid.CreateIfIsValidAndReturnLastIdAsync(dto.Address);
                if (IdAddressCreated > 0)
                    await Create(dto, (uint)IdAddressCreated);
                else
                    apiView.SetValues(
                        dto.Address.Notifications.ToList(),
                        dto.Address.Notifications.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError,
                        false
                    );
            }
        }
        catch
        {
            apiView.SetValues("Falha interna no cadastro", HttpStatusCode.InternalServerError, false);
        }
    }

    private async Task Create(ClientDto dto, uint addressId)
    {
        Client client = dto;
        client.SetAddressId(addressId);

        await _repository.PostAsync(client);
        apiView.SetCode(HttpStatusCode.Created);
    }

    public async Task UpdateIfIsValid(uint id, ClientDto dto)
    {
        try
        {
            if(dto.Validate())
                await UpdateAsync(id, dto);
            else
                apiView.SetValues("Dados inválidos", HttpStatusCode.BadRequest, false);
        }
        catch(Exception ex)
        {
            apiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
        }        
    }

    private async Task UpdateAsync(uint id, ClientDto dto)
    {
        Client client = await _repository.GetAsync(id);

        if (client is null)
        {
            apiView.SetValues("Id de client não encontrado", HttpStatusCode.NotFound, false);
            return;
        }

        await _repository.PutAsync(id, dto);
        apiView.SetCode(HttpStatusCode.NoContent);
    }

    public async Task GetAsync(uint id)
    {
        Client client = await _repository.GetAsync(id);

        if (client is null)
            apiView.SetValues("Id de client não encontrado", HttpStatusCode.NotFound, false);
        else
            apiView.SetData(client);
    }

    public async Task GetAsync(uint limit, uint offetPageNumber)
    {
        IEnumerable<Client>? client = await _repository.GetAsync(limit, offetPageNumber);

        apiView.SetData(client);
    }

    public async Task DeleteAsync(uint id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch
        {
            apiView.SetValues("Falha no delete da entidade", HttpStatusCode.InternalServerError, false);
        }
    }
}
