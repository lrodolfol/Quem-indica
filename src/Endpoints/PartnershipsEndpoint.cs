
using API.Middleware;
using API.Models.Dto;
using API.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Endpoints;

public static class PartnershipsEndpoint
{
    public static void CreatePartnershipsEndpoints(this WebApplication app)
    {
        Post(app);
        Get(app);
        Put(app);
    }

    private static void Post(WebApplication app)
    {
        app.MapPost("/partnership", async ([FromBody] PartnershipCommandDto dto, [FromServices] PartnershipsMid mid) =>
        {
            await mid.TryCreateIfValid(dto);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.Created)
                return Results.Created("GetPartnership", mid.ApiView);
            else if (mid.ApiView.HttpStatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("PostPartnership")
        .Produces((int)HttpStatusCode.Created).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }

    private static void Put(WebApplication app)
    {
        app.MapPut("/partnership/{clientId}/accet-partnership/{partnershipId}", async ([FromRoute] uint clientId, [FromRoute] uint partnershipId, [FromServices] PartnershipsMid mid) =>
        {
            await mid.AcceptRequestPartnershipAsync(clientId, partnershipId);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.Ok(mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("PutPartnership")
        .Produces((int)HttpStatusCode.OK).Produces((int)HttpStatusCode.InternalServerError)
        .WithOpenApi();
    }

    private static void Get(WebApplication app)
    {
        app.MapGet("/partnership/{clientId}", async ([FromRoute] uint clientId, [FromServices] PartnershipsMid mid) =>
        {
            await mid.SearchPartnershipIAmNomiessAsync(clientId);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.Ok(mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("GetPartnership")
        .Produces((int)HttpStatusCode.OK).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();

        app.MapGet("/partnership/{ClientNomieesId}/status/{status}", async ([FromRoute] uint ClientNomieesId, [FromRoute] EPartnershipStatus status, [FromServices] PartnershipsMid mid) =>
        {
            await mid.GetByStatusAsync(ClientNomieesId, status);

            if (mid.ApiView.HttpStatusCode == HttpStatusCode.OK)
                return Results.Ok(mid.ApiView);
            else
                return Results.StatusCode((int)mid.ApiView.HttpStatusCode);
        })
        .WithName("GetPartnershipStatus")
        .Produces((int)HttpStatusCode.OK).Produces((int)HttpStatusCode.InternalServerError).Produces((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }


}
