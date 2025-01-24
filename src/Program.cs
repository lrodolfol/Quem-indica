using API.Configuration;
using API.Endpoints;
using API.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.LoadAppSettings();
builder.LoadDataBaseConnection();
builder.LoadDependencies();
builder.LoadConfiguration();

builder.Services.AddHostedService<TasksServices>();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.CreateClientEndpoints();
app.CreatePartnershipsEndpoints();

app.Run();
