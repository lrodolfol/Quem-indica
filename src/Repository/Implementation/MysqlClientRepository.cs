using API.Models.Entities;
using API.Models.Enums;
using API.Repository.Abstraction;
using Dapper;
using System.Text.Json;

namespace API.Repository.Implementation;

public class MysqlClientRepository : IGenericRepository, IClientRepository, IDisposable
{
    private Connection Connection;
    private string tableName = "client";

    public MysqlClientRepository(Connection connection)
    {
        Connection = connection;
    }

    public Task DeleteAsync<T>(int id) where T : Entitie
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>(int id) where T : Entitie
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public async Task PostAsync<T>(T entity) where T : Entitie
    {
        await Connection._mysqlConnection.OpenAsync();
        var client = ConvertToEntitie(entity);

        if (client is null)
            return;

        var rowsAffedtec = await Connection._mysqlConnection.ExecuteAsync
            (
            $@"INSERT INTO {tableName} ('Name','FictitiousName','Segment','Active') VALUES (@Name,@FictitiousName,@Segment,@Active)",
            client
            );
    }

    public Task PutAsync<T>(int id, T entity) where T : Entitie
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public IEnumerable<Partnerships> SearchPartnershipByClient(int clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }
    private static Client? ConvertToEntitie<T>(T entity) where T : Entitie
    {
        try
        {
            var jsonClient = JsonSerializer.Serialize(entity);
            var client = JsonSerializer.Deserialize<Client>(jsonClient);
            return client;
        }
        catch
        {
            throw;
        }
    }
    public void Dispose()
    {
        Connection._mysqlConnection.CloseAsync();
    }
}
