using API.Models.Dto;
using API.Models.Entities;
using API.Models.ReturnView;
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
            if (await AddressMid.CreateIfIsValid(dto.Address))
                await Create(dto);
            else
                apiView.SetValues("Houve uma falha no cadastro. Contate o time de suporte - SCD851", 500, false);
        }
    }

    private async Task Create(ClientDto dto)
    {
        Client client = dto;
        await _repository.PostAsync(client);
        apiView.SetCode(201);
    }
}
