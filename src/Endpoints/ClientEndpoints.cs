using API.Middleware;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

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
                })
        .WithName("CreateClient")
        .WithOpenApi();
    }
    private static void Get(this WebApplication app)
    {
        app.MapPost("/client/{cliendId}", async ([FromBody] ClientDto dto, [FromQuery] int cliendId) =>
        {
            return "RETORNANDO CLIENTE";
        })
        .WithName("GetClient")
        .WithOpenApi();
    }
}
