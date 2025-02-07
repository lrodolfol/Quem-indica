﻿using API.Models.Dto;
using API.Models.Entities;
using API.Models.Enums;
using API.Repository.Abstraction;
using Dapper;
using System.Data;
using System.Text;

namespace API.Repository.Implementation;

public class MysqlPartnershipRepository : IPartnershipRepository, IBaseRepository
{
    private readonly Connection Connection;

    public MysqlPartnershipRepository(Connection connection)
    {
        Connection = connection;
    }

    private const string TABLENAME = "Partnerships";
    private const string PRIMARYKEYTABLE = "Id";
    private const string FIRSTFOREIGNKEYTABLE = "ClientNomieesId";
    private const string SECONDFOREIGNKEYTABLE = "ClientReferrerId";

    private const string FISRTTABLERELATIONSHIPNAME = "Client";
    private const string FIRSTPRIMARYKEYRELATIONSHIP = "Id";

    public async Task<IEnumerable<PartnershipQueryDto>> SearchPartnershipIAmNomiessAsync(uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED)
    {
        await OpenConnectionIfClose();

        StringBuilder builer = new StringBuilder();
        builer.Append($"SELECT P1.ClientReferrerId, C1.Name, C1.FictitiousName, C1.Segment, P1.CreatedAt, P1.UpdatedAt, P1.ValidUntil ");
        builer.Append($" FROM {TABLENAME} P1 INNER JOIN {FISRTTABLERELATIONSHIPNAME} C1 ON P1.{SECONDFOREIGNKEYTABLE} = C1.{PRIMARYKEYTABLE} ");
        builer.Append(" WHERE P1.ClientNomieesId = @ClientId AND P1.Active = @Active1 AND C1.Active = @Active2 ");
        builer.Append($" AND P1.ValidUntil >= @ValidUntil AND P1.STATUS = @STATUS");
        var parameters = new
        {
            ClientId = clientId,
            Active1 = true,
            Active2 = true,
            ValidUntil = DateTime.UtcNow.ToString("yyyy-MM-dd"),
            Status = nameof(EPartnershipStatus.COMPLETED)
        };

        var entity = await Connection._mysqlConnection.QueryAsync<PartnershipQueryDto>(builer.ToString(), parameters);
        await CloseConnectionIfOpen();

        return entity;
    }

    public async Task PostAsync(PartnershipCommandDto partnerships)
    {
        await OpenConnectionIfClose();

        var query = $"INSERT INTO {TABLENAME} (ClientReferrerId, ClientNomieesId, Active, Status) VALUES (@ClientReferrerId, @ClientNomieesId, @Active, @Status)";
        var parameters = new
        {
            ClientReferrerId = partnerships.ClientReferrerId,
            ClientNomieesId = partnerships.ClientNomieesId,
            Active = true,
            Status = nameof(EPartnershipStatus.PENDING),
        };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
        await CloseConnectionIfOpen();
    }

    public async Task<IEnumerable<PartnershipQueryDto>> GetByStatusAsync(uint clientNomieesId, EPartnershipStatus status)
    {
        await OpenConnectionIfClose();

        StringBuilder builer = new StringBuilder();
        builer.Append($"SELECT P1.ClientReferrerId, C1.Name, C1.FictitiousName, C1.Segment, P1.CreatedAt, P1.UpdatedAt, P1.ValidUntil ");
        builer.Append($" FROM {TABLENAME} P1 INNER JOIN {FISRTTABLERELATIONSHIPNAME} C1 ON P1.{SECONDFOREIGNKEYTABLE} = C1.{PRIMARYKEYTABLE} ");
        builer.Append(" WHERE P1.ClientNomieesId = @ClientId AND P1.Active = @Active1 AND C1.Active = @Active2 AND P1.STATUS = @STATUS");
        var parameters = new
        {
            ClientId = clientNomieesId,
            Status = status.ToString(),
            Active1 = true,
            Active2 = true,
        };

        var entity = await Connection._mysqlConnection.QueryAsync<PartnershipQueryDto>(builer.ToString(), parameters);
        await CloseConnectionIfOpen();

        return entity;
    }

    public async Task SetOverdueStatusAsync(uint daysOverdue)
    {
        await OpenConnectionIfClose();

        var query = $"UPDATE {TABLENAME} P1 SET P1.STATUS = @STATUS WHERE P1.ValidUntil < @NOW ";
        var parameters = new
        {
            STATUS = nameof(EPartnershipStatus.OVERDUE),
            NOW = DateTime.UtcNow.AddDays(daysOverdue),
        };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
        await CloseConnectionIfOpen();
    }

    public async Task AcceptRequestPartnershipAsync(uint clientNomieesId, uint partnershipId)
    {
        await OpenConnectionIfClose();

        var query = $"UPDATE {TABLENAME} P1 SET P1.Status = @newStatus, ValidUntil = @validUntil WHERE P1.{PRIMARYKEYTABLE} = @partnershipId AND P1.Status = @status AND P1.ClientNomieesId = @ClientNomieesId";
        var parameters = new
        {
            newStatus = nameof(EPartnershipStatus.COMPLETED),
            ValidUntil = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"),
            partnershipId = partnershipId,
            status = nameof(EPartnershipStatus.PENDING),
            ClientNomieesId = clientNomieesId
        };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
        await CloseConnectionIfOpen();
    }


    public async Task OpenConnectionIfClose()
    {
        if (Connection._mysqlConnection.State == ConnectionState.Closed)
            await Connection._mysqlConnection.OpenAsync();
    }
    public async Task CloseConnectionIfOpen()
    {
        if(Connection._mysqlConnection.State == ConnectionState.Open)
            await Connection._mysqlConnection.CloseAsync();        
    }
}
