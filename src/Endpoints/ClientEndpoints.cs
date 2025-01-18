using API.Middleware;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Endpoints;

public static class ClientEndpoints
{
    public static void CreateClientEndpoints(this WebApplication app)
    {
        PostAsync(app);
        GetAsync(app);
        PutAsync(app);
    }

    private static void PostAsync(this WebApplication app)
    {
        app.MapPost("/client", async ([FromBody] ClientDto dto, [FromServices] ClientMid mid) =>
                {
                    await mid.CreateIfIsValid(dto);

                    if (mid.apiView.HttpStatusCode == HttpStatusCode.Created)
                        return Results.Created("GetClient", mid.apiView);
                    else if (mid.apiView.HttpStatusCode == HttpStatusCode.BadRequest)
                        return Results.BadRequest(mid.apiView);
                    else
                        return Results.StatusCode((int)mid.apiView.HttpStatusCode);
                })
        .WithName("CreateClient")
        .Produces((int)HttpStatusCode.Created).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }
    private static void GetAsync(this WebApplication app)
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

    private static void PutAsync(WebApplication app)
    {
        app.MapPut("client/{clientId}", async ([FromQuery] uint clientId, [FromBody] ClientDto dto, [FromServices] ClientMid mid) =>
        {
            await mid.UpdateIfIsValid(clientId, dto);

            if (mid.apiView.HttpStatusCode == HttpStatusCode.NoContent)
                return Results.NoContent();
            else if(mid.apiView.HttpStatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(mid.apiView);
            else 
                return Results.StatusCode((int)mid.apiView.HttpStatusCode);
        });
    }

}
