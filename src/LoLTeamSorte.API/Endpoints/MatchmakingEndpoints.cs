using Carter;
using LoLTeamSorter.Application.Commands.GenerateMatchmaking;
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
        }
    }
}
