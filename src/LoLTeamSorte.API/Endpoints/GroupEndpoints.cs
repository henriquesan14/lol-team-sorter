using Carter;
using LoLTeamSorter.Application.Queries.GetGroups;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorte.API.Endpoints
{
    public class GroupEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/groups");

            group.MapGet("/", [Authorize(Policy = "CreateUser")] async (ISender sender) =>
            {
                var query = new GetGroupsQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
