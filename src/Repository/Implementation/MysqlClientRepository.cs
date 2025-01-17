using API.Models.Entities;
using API.Models.Enums;
using API.Models.ValueObjects;
using API.Repository.Abstraction;
using Dapper;

namespace API.Repository.Implementation;

public class MysqlClientRepository : IClientRepository, IDisposable
{
    private Connection Connection;
    private const string TABLENAME = "Client";
    private const string PRIMARYKEYTABLE = "Id";
    private const string FIRSTFOREIGNKEYTABLE = "AddressId";

    private const string FISRTTABLERELATIONSHIPNAME = "Address";
    private const string FIRSTPRIMARYKEYRELATIONSHIP = "Id";


    public MysqlClientRepository(Connection connection)
    {
        Connection = connection;
    }

    public Task DeleteAsync(uint id)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public async Task<Client> GetAsync(uint id)
    {
        await Connection._mysqlConnection.OpenAsync();
        var parameters = new { Id = id };
        string queryBuilder = $"SELECT * FROM {TABLENAME} t INNER JOIN {FISRTTABLERELATIONSHIPNAME} r ON t.{FIRSTFOREIGNKEYTABLE} = r.{FIRSTPRIMARYKEYRELATIONSHIP} WHERE t.{PRIMARYKEYTABLE} = {@id}";
        IEnumerable<Client> entity = await Connection._mysqlConnection.QueryAsync(queryBuilder, GetEntityWithRelationShip(), parameters);

        return entity.First();
    }
    public async Task<IEnumerable<Client>> GetAsync(uint limit, uint offset)
    {
        await Connection._mysqlConnection.OpenAsync();
        string queryBuilder = $"SELECT * FROM {TABLENAME} t INNER JOIN {FISRTTABLERELATIONSHIPNAME} r ON t.{FIRSTFOREIGNKEYTABLE} = r.{FIRSTPRIMARYKEYRELATIONSHIP} LIMIT {limit} OFFSET {offset}";
        IEnumerable<Client> entity = await Connection._mysqlConnection.QueryAsync(queryBuilder, GetEntityWithRelationShip());

        return entity;
    }

    public async Task PostAsync(Client entity)
    {
        await Connection._mysqlConnection.OpenAsync();

        var rowsAffedtec = await Connection._mysqlConnection.ExecuteAsync
            (
            $@"INSERT INTO {TABLENAME} (Name,FictitiousName,Segment,Active,AddressId) VALUES (@Name,@FictitiousName,@Segment,@Active,@AddressId)",
            entity
            );
    }

    public Task PutAsync(uint id, Client entity)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public IEnumerable<Partnerships> SearchPartnershipByClient(uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    private static Func<Client, Address, Client> GetEntityWithRelationShip()
    {
        return (client, address) =>
        {
            if (address != null)
                client.SetAddress(address);
            else
                client.SetAddress(new Address());

            return client;
        };
    }

    public void Dispose()
    {
        Connection._mysqlConnection.CloseAsync();
    }
}
