using API.Models.Dto;
using API.Models.Entities;
using API.Models.ReturnView;
using API.Repository.Implementation;

namespace API.Middleware;

public class ClientMid
{
    private readonly MysqlClientRepository _repository;
    public ApiView apiView { get; set; }

    public ClientMid(MysqlClientRepository repository)
    {
        _repository = repository;
        apiView = new ApiView();
    }

    public async Task CreateIfIsValid(ClientDto dto)
    {
        if (!dto.Validate())
            apiView.SetValues(dto.Notifications.ToList(), 400, false);
        else
            await Create(dto);
    }

    private async Task Create(ClientDto dto)
    {
        Client client = dto;
        await _repository.PostAsync(client);
        apiView.SetCode(201);
    }
}
