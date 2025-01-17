using API.Middleware;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Endpoints;

public static class ClientEndpoints
{
    public static void CreateClientEndpoints(this WebApplication app)
    {
        Post(app);
        Get(app);
    }

    private static void Post(this WebApplication app)
    {
        app.MapPost("/client", async ([FromBody] ClientDto dto, [FromServices] ClientMid mid) =>
                {
                    await mid.CreateIfIsValid(dto);

                    return mid.apiView.HttpStatusCode == HttpStatusCode.Created ?
                        Results.Created("GetClient", mid.apiView) :
                        Results.StatusCode((int)mid.apiView.HttpStatusCode);
                })
        .WithName("CreateClient")
        .Produces((int)HttpStatusCode.Created).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }
    private static void Get(this WebApplication app)
    {
        app.MapGet("/client/{cliendId}", async ([FromQuery] uint cliendId, [FromServices] ClientMid mid) =>
        {
            await mid.GetAsync(cliendId);
            return mid.apiView;
        })
        .WithName("GetClient")
        .WithOpenApi();

        app.MapGet("/client/", async ([FromServices] ClientMid mid, [FromQuery] uint limit = 25, [FromQuery] uint offset = 0) =>
        {
            await mid.GetAsync(limit, offset);
            return mid.apiView;
        })
        .WithName("GetClients")
        .WithOpenApi();
    }

}
