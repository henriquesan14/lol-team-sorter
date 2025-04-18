using Carter;
using LoLTeamSorter.Application.Commands.DeleteMatchmaking;
using LoLTeamSorter.Application.Commands.DeleteMatchmakings;
using LoLTeamSorter.Application.Commands.DeletePlayer;
using LoLTeamSorter.Application.Commands.DeletePlayers;
using LoLTeamSorter.Application.Commands.GenerateMatchmaking;
using LoLTeamSorter.Application.Pagination;
using LoLTeamSorter.Application.Queries.GetMatchmakings;
using LoLTeamSorter.Domain.Enums;
using MediatR;

namespace LoLTeamSorte.API.Endpoints
{
    public class MatchmakingEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/matchmaking");

            group.MapPost("/", async (GenerateMatchmakingCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result);
            });

            group.MapGet("/", async ([AsParameters] PaginationRequest request, ModeEnum? mode, DateTime? startDate, DateTime? endDate, ISender sender) =>
            {
                var query = new GetMatchmakingsQuery(mode, startDate, endDate, request.PageNumber, request.PageSize);
                var result = await sender.Send(query);

                return Results.Ok(result);
            });

            group.MapDelete("/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteMatchmakingCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", async (DeleteMatchmakingsCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
