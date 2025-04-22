using LoLTeamSorter.API;
using LoLTeamSorter.Application;
using LoLTeamSorter.Infra;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddHealthChecks();

var configuration = builder.Configuration;

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddApiServices(configuration);

var app = builder.Build();

app.UseHealthChecks("/health");

app.UseApiServices();

app.Run();
