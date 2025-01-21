
using API.Middleware;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Endpoints;

public static class PartnershipsEndpoint
{
    public static void CreatePartnershipsEndpoints(this WebApplication app)
    {
        Post(app);
        Get(app);
    }

    private static void Post(WebApplication app)
    {
        app.MapPost("/partnership", async ([FromBody] PartnershipCommandDto dto, [FromServices] PartnershipsMid mid) =>
        {
            await mid.Post(dto);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.Created)
                return Results.Created("GetPartnership", mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("PostPartnership")
        .Produces((int)HttpStatusCode.Created).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }

    private static void Get(WebApplication app)
    {
        app.MapGet("/partnership/{clientId}", async ([FromRoute] uint clientId, [FromServices] PartnershipsMid mid) =>
        {
            await mid.GetAsync(clientId);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.Ok(mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("GetPartnership")
        .Produces((int)HttpStatusCode.OK).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }
}
