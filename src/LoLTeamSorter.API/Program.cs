using LoLTeamSorter.API;
using LoLTeamSorter.Application;
using LoLTeamSorter.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

Console.WriteLine($"DB_HOST: {Environment.GetEnvironmentVariable("DB_HOST")}");
Console.WriteLine($"DB_PORT: {Environment.GetEnvironmentVariable("DB_PORT")}");
Console.WriteLine($"DB_USER: {Environment.GetEnvironmentVariable("DB_USER")}");
Console.WriteLine($"DB_PASS: {Environment.GetEnvironmentVariable("DB_PASS")}");
Console.WriteLine($"RIOT_API_KEY: {Environment.GetEnvironmentVariable("RIOT_API_KEY")}");
Console.WriteLine($"JWT_SECRET: {Environment.GetEnvironmentVariable("JWT_SECRET")}");

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
