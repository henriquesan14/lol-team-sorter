using Carter;
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
        }
    }
}
