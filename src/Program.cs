using API.Configuration;
using API.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.LoadAppSettings();
builder.LoadDataBaseConnection();
builder.LoadDependencies();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.CreateClientEndpoints();

app.Run();
