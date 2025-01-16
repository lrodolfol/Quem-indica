using API.Models.ValueObjects;
using API.Repository.Abstraction;
using Dapper;

namespace API.Repository.Implementation;

public class MysqlAddressRepository : IAddressRepository, IDisposable
{
    private Connection Connection;
    private string tableName = "address";

    public MysqlAddressRepository(Connection connection)
    {
        Connection = connection;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task PostAsync(Address entity)
    {
        await Connection._mysqlConnection.OpenAsync();

        var rowsAffedtec = await Connection._mysqlConnection.ExecuteReaderAsync
            (
            $@"INSERT INTO {tableName} (Country,State,City,ZipCode,District,Street,Additional,Number,Active) VALUES (@Country,@State,@City,@ZipCode,@District,@Street,@Additional,@Number,@Active)",
            entity
            );
    }

    public Task PutAsync(int id, Address entity)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Connection._mysqlConnection.CloseAsync();
    }
}
