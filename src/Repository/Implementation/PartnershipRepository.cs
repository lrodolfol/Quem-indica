using API.Models.Dto;
using API.Models.Entities;
using API.Models.Enums;
using API.Models.ValueObjects;
using API.Repository.Abstraction;
using Dapper;
using System.Data;
using System.Text;

namespace API.Repository.Implementation;

public class PartnershipRepository : IPartnershipRepository
{
    private readonly Connection Connection;

    public PartnershipRepository(Connection connection)
    {
        Connection = connection;
    }

    private const string TABLENAME = "Partnerships";
    private const string PRIMARYKEYTABLE = "Id";
    private const string FIRSTFOREIGNKEYTABLE = "ClientNomieesId";
    private const string SECONDFOREIGNKEYTABLE = "ClientReferrerId";

    private const string FISRTTABLERELATIONSHIPNAME = "Client";
    private const string FIRSTPRIMARYKEYRELATIONSHIP = "Id";

    public async Task<IEnumerable<PartnershipQueryDto>> SearchPartnershipByClient(uint clientId, EPartnershipStatus status = EPartnershipStatus.COMPLETED)
    {
        await OpenDatabaseIfClose();
        
        StringBuilder builer = new StringBuilder();
        builer.Append($"SELECT P1.ClientReferrerId, C1.Name, C1.FictitiousName, C1.Segment, P1.CreatedAt, P1.UpdatedAt, P1.ValidUntil ");
        builer.Append($" FROM {TABLENAME} P1 INNER JOIN {FISRTTABLERELATIONSHIPNAME} C1 ON P1.{SECONDFOREIGNKEYTABLE} = C1.{PRIMARYKEYTABLE} ");
        builer.Append(" WHERE P1.ClientNomieesId = @ClientId AND P1.Active = @Active1 AND C1.Active = @Active2 ");
        builer.Append($" AND P1.ValidUntil >= @ValidUntil");
        var parameters = new
        {
            ClientId = clientId,
            Active1 = true,
            Active2 = true,
            ValidUntil = DateTime.UtcNow.ToString("yyyy-MM-dd")
        };

        var entity = await Connection._mysqlConnection.QueryAsync<PartnershipQueryDto>(builer.ToString(), parameters);

        return entity;
    }

    public async Task PostAsync(PartnershipCommandDto partnerships)
    {
        await OpenDatabaseIfClose();

        var query = $"INSERT INTO {TABLENAME} (ClientReferrerId, ClientNomieesId, Active) VALUES (@ClientReferrerId, @ClientNomieesId, @Active)";
        var parameters = new
        {
            ClientReferrerId = partnerships.ClientReferrerId,
            ClientNomieesId = partnerships.ClientNomieesId,
            Active = true,
        };

        await Connection._mysqlConnection.ExecuteAsync(query, parameters);
    }

    private async Task OpenDatabaseIfClose()
    {
        if (Connection._mysqlConnection.State == ConnectionState.Closed)
            await Connection._mysqlConnection.OpenAsync();
    }
}
