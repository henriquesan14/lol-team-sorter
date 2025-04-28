using Carter;
using LoLTeamSorter.Application.Commands.CreateGroup;
using LoLTeamSorter.Application.Commands.DeleteGroup;
using LoLTeamSorter.Application.Commands.DeleteGroups;
using LoLTeamSorter.Application.Commands.UpdateGroup;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Application.Queries.GetGroupById;
using LoLTeamSorter.Application.Queries.GetGroups;
using LoLTeamSorter.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorter.API.Endpoints
{
    public class GroupEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/groups");

            group.MapGet("/", [Authorize(Policy = "ViewUser")] async (ISender sender) =>
            {
                var query = new GetGroupsQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithName("GetGroups")
                .Produces<List<GroupViewModel>>(StatusCodes.Status200OK);

            group.MapGet("/{id}", [Authorize(Policy = "ViewUser")] async (Guid id, ISender sender) =>
            {
                var query = new GetGroupByIdQuery(id);
                var result = await sender.Send(query);

                return Results.Ok(result);
            })
                .WithName("GetGroupById")
                .Produces<GroupViewModel>(StatusCodes.Status200OK);

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
