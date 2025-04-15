using Carter;
using LoLTeamSorter.Application.Commands.CreatePlayer;
using LoLTeamSorter.Application.Queries.GetPlayers;
using MediatR;

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
        }
    }
}
