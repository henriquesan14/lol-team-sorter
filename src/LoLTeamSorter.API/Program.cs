using LoLTeamSorter.API;
using LoLTeamSorter.Application;
using LoLTeamSorter.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureHostUrls();

var configuration = builder.Configuration;

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddApiServices(builder, configuration);

var app = builder.Build();

app.UseApiServices(configuration);

app.Run();
