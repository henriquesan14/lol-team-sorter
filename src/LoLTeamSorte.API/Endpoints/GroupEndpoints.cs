using Carter;
using LoLTeamSorter.Application.Commands.CreateGroup;
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

            group.MapPost("/", [Authorize(Policy = "CreateUser")] async (CreateGroupCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"groups/{result}", result);
            });
        }
    }
}
