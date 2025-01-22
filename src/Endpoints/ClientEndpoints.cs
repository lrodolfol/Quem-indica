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
        DeleteAsync(app);
    }

    private static void PostAsync(this WebApplication app)
    {
        app.MapPost("/client", async ([FromBody] ClientDto dto, [FromServices] ClientMid mid) =>
                {
                    await mid.CreateIfIsValid(dto);

                    if (mid.ApiView.HttpStatusCode == HttpStatusCode.Created)
                        return Results.Created("GetClient", mid.ApiView);
                    else if (mid.ApiView.HttpStatusCode == HttpStatusCode.BadRequest)
                        return Results.BadRequest(mid.ApiView);
                    else
                        return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
                })
        .WithName("CreateClient")
        .Produces((int)HttpStatusCode.Created).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }

    private static void GetAsync(this WebApplication app)
    {
        app.MapGet("/client/{clientId}", async ([FromRoute] uint clientId, [FromServices] ClientMid mid) =>
        {
            await mid.GetAsync((uint)clientId);
            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.Ok(mid.ApiView);
            else
                return Results.NotFound(mid.ApiView);
        })
        .WithName("GetClient")
        .Produces((int)HttpStatusCode.NoContent).Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapGet("/client/", async ([FromServices] ClientMid mid, [FromQuery] uint limit = 25, [FromQuery] uint offset = 0) =>
        {
            await mid.GetAsync(limit, offset);
            return mid.ApiView;
        })
        .WithName("GetClients")
        .Produces((int)HttpStatusCode.NoContent).Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();
    }

    private static void PutAsync(WebApplication app)
    {
        app.MapPut("client/{clientId}", async ([FromRoute] uint clientId, [FromBody] ClientDto dto, [FromServices] ClientMid mid) =>
        {
            await mid.UpdateIfIsValid(clientId, dto);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.NoContent();
            else if(mid.ApiView.HttpStatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(mid.ApiView);
            else 
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("PutClient")
        .WithGroupName("Client")
        .Produces((int)HttpStatusCode.NoContent).Produces((int)HttpStatusCode.BadRequest).Produces((int)HttpStatusCode.InternalServerError)
        .WithOpenApi();
    }

    private static void DeleteAsync(WebApplication app)
    {
        app.MapDelete("/client/{clientId}", async ([FromQuery] uint cliendId, [FromServices] ClientMid mid) =>
        {
            await mid.DeleteAsync(cliendId);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.InternalServerError)
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
            else 
                return Results.Ok(mid.ApiView);
        })
        .WithName("DeleteClient")
        .Produces((int)HttpStatusCode.NoContent).Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();
    }
}
