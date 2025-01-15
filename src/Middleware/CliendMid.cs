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

    public async Task Create(Client client)
    {
        _repository.PostAsync(client);
    }
}
