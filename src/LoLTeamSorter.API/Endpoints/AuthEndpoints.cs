using Carter;
using LoLTeamSorter.Application.Commands.GenerateAccessToken;
using LoLTeamSorter.Application.Commands.LoginDiscord;
using LoLTeamSorter.Application.Commands.RenewRefreshToken;
using LoLTeamSorter.Application.Commands.RevokeRefreshToken;
using MediatR;

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

                return Results.Redirect(result.RedirectAppUrl!);
            });

            group.MapPost("/refresh-token", async (RefreshTokenCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result);
            });

            group.MapPost("/logout", async (RevokeRefreshTokenCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
