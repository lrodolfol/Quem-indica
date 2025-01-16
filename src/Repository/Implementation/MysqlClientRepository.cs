using API.Models.Entities;
using API.Models.Enums;
using API.Repository.Abstraction;
using Dapper;

namespace API.Repository.Implementation;

public class MysqlClientRepository : IClientRepository, IDisposable
{
    private Connection Connection;
    private string tableName = "client";

    public MysqlClientRepository(Connection connection)
    {
        Connection = connection;
    }

    public Task DeleteAsync(int id)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public Task<Client> GetAsync(int id)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public async Task PostAsync(Client entity)
    {
        await Connection._mysqlConnection.OpenAsync();

        var rowsAffedtec = await Connection._mysqlConnection.ExecuteAsync
            (
            $@"INSERT INTO {tableName} (Name,FictitiousName,Segment,Active,AddressId) VALUES (@Name,@FictitiousName,@Segment,@Active,@AddressId)",
            entity
            );
    }

    public Task PutAsync(int id, Client entity)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public IEnumerable<Partnerships> SearchPartnershipByClient(int clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Connection._mysqlConnection.CloseAsync();
    }
}
