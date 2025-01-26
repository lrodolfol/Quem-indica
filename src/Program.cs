using API.Configuration;
using API.Endpoints;
using API.Middleware;
using Hangfire;
using Hangfire.MySql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.LoadAppSettings();
builder.LoadDataBaseConnection();
builder.LoadDependencies();
builder.LoadConfiguration();

//builder.Services.AddHostedService<TasksServices>();

WebApplication app = builder.Build();
app.UseHangfireDashboard();

var serviceProvider = builder.Services.BuildServiceProvider();
var myService = serviceProvider.GetRequiredService<PartnershipsMid>();
RecurringJob.AddOrUpdate("SetStatusOverdue", 
    () => myService.SetOverdueStatusAsync(ConfigFromAppSettings.SectionConfig.DaysForOverdue), 
    Cron.Daily(03, 15)
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.CreateClientEndpoints();
app.CreatePartnershipsEndpoints();

app.Run();
