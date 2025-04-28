using Carter;
using LoLTeamSorter.Application.Commands.CreatePlayer;
using LoLTeamSorter.Application.Commands.DeletePlayer;
using LoLTeamSorter.Application.Commands.DeletePlayers;
using LoLTeamSorter.Application.Commands.UpdatePlayer;
using LoLTeamSorter.Application.Commands.UpdateRankedTier;
using LoLTeamSorter.Application.Commands.UpdateRankedTiers;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Application.Queries.GetChampionMasteries;
using LoLTeamSorter.Application.Queries.GetChampionRankedStats;
using LoLTeamSorter.Application.Queries.GetPlayers;
using LoLTeamSorter.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorter.API.Endpoints
{
    public class PlayerEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/players");

            group.MapPost("/", [Authorize(Policy = "CreatePlayer")] async (CreatePlayerCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"players/{result}", result);
            });

            group.MapGet("/", [Authorize(Policy = "ViewPlayer")] async ([AsParameters] GetPlayersQuery query, ISender sender) =>
            {
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithName("GetPlayers")
                .Produces<IEnumerable<PlayerViewModel>>(StatusCodes.Status200OK);

            group.MapPut("/", [Authorize(Policy = "EditPlayer")] async (UpdatePlayerCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", [Authorize(Policy = "DeletePlayer")] async (Guid id, ISender sender) =>
            {
                var command = new DeletePlayerCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", [Authorize(Policy = "DeletePlayer")] async (DeletePlayersCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPatch("/{id}", [Authorize(Policy = "UpdateRankedTierPlayer")] async (Guid Id, ISender sender) =>
            {
                var command = new UpdateRankedTierCommand(Id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/update-ranked-tiers", [Authorize(Policy = "UpdateRankedTierPlayer")] async (UpdateRankedTiersCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            });

            group.MapGet("/{riotId}/championMasteries/", [Authorize(Policy = "ViewPlayer")] async (string riotId, ISender sender) =>
            {
                var query = new GetChampionMasteriesQuery(riotId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
                .WithName("GetChampionMasteries")
                .Produces<List<ChampionMasteryDto>>(StatusCodes.Status200OK);

            group.MapGet("/{riotId}/champion-ranked-stats", [Authorize(Policy = "ViewPlayer")] async (string riotId, ISender sender) =>
            {
                var query = new GetChampionRankedStatsQuery(riotId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
                .WithName("GetChampionRankedStats")
                .Produces<List<ChampionRankedStatsDto>>(StatusCodes.Status200OK);
        }
    }
}
