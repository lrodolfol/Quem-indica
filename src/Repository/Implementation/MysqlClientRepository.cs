using API.Models.Entities;
using API.Models.Enums;
using API.Repository.Abstraction;

namespace API.Repository.Implementation;

public class MysqlClientRepository : IGenericRepository, IClientRepository, IDisposable
{
    private Connection Connection;

    public MysqlClientRepository(Connection connection)
    {
        Connection = connection;
    }

    public Task DeleteAsync<T>(int id)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>(int id)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public Task PostAsync<T>(T entity)
    {
        Connection._mysqlConnection.OpenAsync();
        throw new NotImplementedException();
    }

    public Task PutAsync<T>(int id, T entity)
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
