using Carter;
using LoLTeamSorter.Application.Commands.CreateGroup;
using LoLTeamSorter.Application.Commands.DeleteGroup;
using LoLTeamSorter.Application.Commands.DeleteGroups;
using LoLTeamSorter.Application.Commands.DeleteUser;
using LoLTeamSorter.Application.Commands.DeleteUsers;
using LoLTeamSorter.Application.Commands.UpdateGroup;
using LoLTeamSorter.Application.Queries.GetGroupById;
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
            group.MapGet("/{id}", [Authorize(Policy = "CreateUser")] async (Guid id, ISender sender) =>
            {
                var query = new GetGroupByIdQuery(id);
                var result = await sender.Send(query);

                return Results.Ok(result);
            });

            group.MapPost("/", [Authorize(Policy = "CreateUser")] async (CreateGroupCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"groups/{result}", result);
            });

            group.MapPut("/", [Authorize(Policy = "CreateUser")] async (UpdateGroupCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", [Authorize(Policy = "CreateUser")] async (Guid id, ISender sender) =>
            {
                var command = new DeleteGroupCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", [Authorize(Policy = "CreateUser")] async (DeleteGroupsCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
