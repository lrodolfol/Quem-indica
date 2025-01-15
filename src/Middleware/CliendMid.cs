using API.Models.Dto;
using API.Models.Entities;
using API.Repository.Implementation;

namespace API.Middleware;

public class CliendMid
{
    private readonly MysqlClientRepository _repository;

    public CliendMid(MysqlClientRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(ClientDto dto)
    {
        Client client = dto;
        await _repository.PostAsync(client);
    }
}
