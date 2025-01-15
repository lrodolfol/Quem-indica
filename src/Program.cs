using API.Configuration;
using API.Middleware;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.LoadAppSettings();
builder.LoadDataBaseConnection();
builder.LoadDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//===MAPEAMENTO DAS APIS
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/client", async ([FromBody ]ClientDto dto, [FromServices] ClientMid mid) =>
{
    await mid.CreateIfIsValid(dto);
})
.WithName("CreateClient")
.WithOpenApi();
//===FIM MAPEAMENTO DAS APIS

app.Run();
