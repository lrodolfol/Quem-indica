﻿using API.Models.Entities;
using API.Models.Enums;
using API.Models.ValueObjects;
using API.Repository.Abstraction;
using Dapper;
using System.Data;

namespace API.Repository.Implementation;

public class MysqlClientRepository : IClientRepository, IBaseRepository
{
    private readonly Connection Connection;
    private const string TABLENAME = "Client";
    private const string PRIMARYKEYTABLE = "Id";
    private const string FIRSTFOREIGNKEYTABLE = "AddressId";

    private const string FISRTTABLERELATIONSHIPNAME = "Address";
    private const string FIRSTPRIMARYKEYRELATIONSHIP = "Id";


    public MysqlClientRepository(Connection connection)
    {
        Connection = connection;
    }

    public async Task DeleteAsync(uint id)
    {
        await OpenConnectionIfClose();

        var query = $"DELETE FROM {TABLENAME} WHERE {PRIMARYKEYTABLE} = @Id";
        var parameters = new { Id = id };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
        await CloseConnectionIfOpen();
    }

    public async Task<Client?> GetAsync(uint id)
    {
        await OpenConnectionIfClose();

        var parameters = new { Id = id };
        string queryBuilder = $"SELECT * FROM {TABLENAME} t INNER JOIN {FISRTTABLERELATIONSHIPNAME} r ON t.{FIRSTFOREIGNKEYTABLE} = r.{FIRSTPRIMARYKEYRELATIONSHIP} WHERE t.{PRIMARYKEYTABLE} = @id";
        IEnumerable<Client> entity = await Connection._mysqlConnection.QueryAsync(queryBuilder, GetEntityWithRelationShip(), parameters);
        await CloseConnectionIfOpen();

        return entity is not null ? entity.First() : null;
    }

    public async Task<IEnumerable<Client>> GetAsync(uint limit, uint offset)
    {
        await OpenConnectionIfClose();

        string queryBuilder = $"SELECT * FROM {TABLENAME} t INNER JOIN {FISRTTABLERELATIONSHIPNAME} r ON t.{FIRSTFOREIGNKEYTABLE} = r.{FIRSTPRIMARYKEYRELATIONSHIP} LIMIT {limit} OFFSET {offset}";
        IEnumerable<Client> entity = await Connection._mysqlConnection.QueryAsync(queryBuilder, GetEntityWithRelationShip());
        await CloseConnectionIfOpen();

        return entity;
    }

    public async Task PostAsync(Client entity)
    {
        await OpenConnectionIfClose();

        var query = $@"INSERT INTO {TABLENAME} (Name,FictitiousName,Segment,Active,AddressId) VALUES (@Name,@FictitiousName,@Segment,@Active,@AddressId)";
        var parameters = new
        {
            entity.Name,
            entity.FictitiousName,
            Segment = entity.Segment.ToString(),
            entity.Active,
            entity.AddressId
        };
        await Connection._mysqlConnection.ExecuteAsync(query,parameters);
        await CloseConnectionIfOpen();
    }

    public async Task PutAsync(uint id, Client entity)
    {
        await OpenConnectionIfClose();

        var query = $"UPDATE {TABLENAME} C SET Name = @Name, FictitiousName = @FictitiousName, Segment = @Segment, Active = @Active WHERE C.{PRIMARYKEYTABLE} = @id";
        var parameters = new {
            entity.Name, 
            entity.FictitiousName, 
            entity.Segment,
            entity.Active,
            Id =  id
        };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
        await CloseConnectionIfOpen();
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
