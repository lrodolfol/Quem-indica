using API.Models.ValueObjects;
using API.Repository.Abstraction;
using Dapper;
using System.Data;

namespace API.Repository.Implementation;

public class MysqlAddressRepository : IAddressRepository, IBaseRepository
{
    private readonly Connection Connection;
    private const string tableName = "Address";

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

    public async Task<int> PostReturnLastIdAsync(Address entity)
    {
        await OpenConnectionIfClose();

        var lastId = await Connection._mysqlConnection.ExecuteScalarAsync<int>
            (
            $@"INSERT INTO {tableName} (Country,State,City,ZipCode,District,Street,Additional,Number,Active) VALUES (@Country,@State,@City,@ZipCode,@District,@Street,@Additional,@Number,@Active);SELECT LAST_INSERT_ID();",
            entity
            );

        await Connection._mysqlConnection.CloseAsync();

        return lastId;
    }

    public Task PutAsync(int id, Address entity)
    {
        throw new NotImplementedException();
    }

    public async Task OpenConnectionIfClose()
    {
        if (Connection._mysqlConnection.State == ConnectionState.Closed)
            await Connection._mysqlConnection.OpenAsync();
    }
    public async Task CloseConnectionIfOpen()
    {
        if (Connection._mysqlConnection.State == ConnectionState.Open)
            await Connection._mysqlConnection.CloseAsync();
    }
}
