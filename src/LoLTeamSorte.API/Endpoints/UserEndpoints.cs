using Carter;
using LoLTeamSorter.Application.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorte.API.Endpoints
{
    public class UserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            group.MapGet("/", [Authorize(Policy = "ViewUser")] async ( ISender sender) =>
            {
                var query = new GetUsersQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
