using Carter;
using LoLTeamSorter.Application.Commands.GenerateAccessToken;
using LoLTeamSorter.Application.Commands.LoginDiscord;
using MediatR;
using System.Text;
using System.Text.Json;

namespace LoLTeamSorter.API.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/", async (GenerateAccessTokenCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result);
            });

            group.MapGet("/discord/callback", async (string code, ISender sender) =>
            {
                LoginDiscordCommand command = new LoginDiscordCommand(code);
                var result = await sender.Send(command);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(result, options);
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

                return Results.Redirect($"http://localhost:4200/auth/callback#data={base64}");
            });
        }
    }
}
