using Carter;
using LoLTeamSorter.Application.Commands.GenerateAccessToken;
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
        }
    }
}
