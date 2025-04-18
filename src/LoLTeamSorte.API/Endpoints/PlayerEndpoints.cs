using Carter;
using LoLTeamSorter.Application.Commands.CreatePlayer;
using LoLTeamSorter.Application.Commands.DeletePlayer;
using LoLTeamSorter.Application.Commands.DeletePlayers;
using LoLTeamSorter.Application.Commands.UpdatePlayer;
using LoLTeamSorter.Application.Commands.UpdateRankedTier;
using LoLTeamSorter.Application.Commands.UpdateRankedTiers;
using LoLTeamSorter.Application.Queries.GetChampionMasteries;
using LoLTeamSorter.Application.Queries.GetChampionRankedStats;
using LoLTeamSorter.Application.Queries.GetPlayers;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LoLTeamSorte.API.Endpoints
{
    public class PlayerEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/players");

            group.MapPost("/", async (CreatePlayerCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"players/{result}", result);
            });

            group.MapGet("/", async (ISender sender) =>
            {
                var query = new GetPlayersQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });

            group.MapPut("/", async (UpdatePlayerCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeletePlayerCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", async (DeletePlayersCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPatch("/{id}", async (Guid Id, ISender sender) =>
            {
                var command = new UpdateRankedTierCommand(Id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/update-ranked-tiers", async (UpdateRankedTiersCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            });

            group.MapGet("/{riotId}/championMasteries/", async (string riotId, ISender sender) =>
            {
                var query = new GetChampionMasteriesQuery(riotId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            });

            group.MapGet("/{riotId}/champion-ranked-stats", async (string riotId, ISender sender) =>
            {
                var query = new GetChampionRankedStatsQuery(riotId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
