using API.Models.Dto;
using API.Models.Entities;
using API.Models.ReturnView;
using API.Repository.Abstraction;
using System.Net;

namespace API.Middleware;

public class ClientMid
{
    private readonly IClientRepository _repository;
    private readonly AddressMid _addressMid;
    private readonly ILogger<ClientMid> _logger;

    public ApiView ApiView { get; private set; }

    public ClientMid(IClientRepository repository, AddressMid addressMid, ILogger<ClientMid> logger)
    {
        _repository = repository;
        _logger = logger;
        _addressMid = addressMid;
        
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
                var IdAddressCreated = await _addressMid.CreateIfIsValidAndReturnLastIdAsync(dto.Address);
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
        catch(Exception ex)
        {
            ApiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
            _logger.LogError("Falha no processo #0bd72388. Erro: {erro}", ex.Message);
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
        catch(Exception ex)
        {
            ApiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
            _logger.LogError("Falha no processo #e43d5c1c. Erro: {erro}", ex.Message);
        }        
    }

    private async Task UpdateAsync(uint id, ClientDto dto)
    {
        try
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
        catch (Exception ex)
        {
            ApiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
            _logger.LogError("Falha no processo #cb6e4da7. Erro: {erro}", ex.Message);
        }
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
        catch (Exception ex)
        {
            ApiView.SetValues("Falha interna na atualização", HttpStatusCode.InternalServerError, false);
            _logger.LogError("Falha no processo #50485669. Erro: {erro}", ex.Message);
        }
    }
}
