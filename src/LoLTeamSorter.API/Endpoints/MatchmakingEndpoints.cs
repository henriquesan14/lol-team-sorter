using Carter;
using LoLTeamSorter.Application.Commands.DeleteMatchmaking;
using LoLTeamSorter.Application.Commands.DeleteMatchmakings;
using LoLTeamSorter.Application.Commands.FinishMatch;
using LoLTeamSorter.Application.Commands.GenerateMatchmaking;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Application.Pagination;
using LoLTeamSorter.Application.Queries.GetMatchmakings;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorter.API.Endpoints
{
    public class MatchmakingEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/matchmaking");

            group.MapPost("/", [Authorize(Policy = "GenerateMatchmaking")] async (GenerateMatchmakingCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result);
            });

            group.MapGet("/", [Authorize(Policy = "ViewMatchmaking")] async ([AsParameters] PaginationRequest request, ModeEnum? mode, DateTime? startDate, DateTime? endDate, ISender sender) =>
            {
                var query = new GetMatchmakingsQuery(mode, startDate, endDate, request.PageNumber, request.PageSize);
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithName("GetMatchmakings")
                .Produces<List<MatchmakingViewModel>>(StatusCodes.Status200OK);

            group.MapDelete("/{id}", [Authorize(Policy = "DeleteMatchmaking")] async (Guid id, ISender sender) =>
            {
                var command = new DeleteMatchmakingCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", [Authorize(Policy = "DeleteMatchmaking")] async (DeleteMatchmakingsCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/finish", [Authorize(Policy = "ViewMatchmaking")] async (FinishMatchCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
