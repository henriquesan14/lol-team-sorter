using Carter;
using LoLTeamSorter.Application.Commands.CreateUser;
using LoLTeamSorter.Application.Commands.DeleteUser;
using LoLTeamSorter.Application.Commands.DeleteUsers;
using LoLTeamSorter.Application.Commands.UpdateUser;
using LoLTeamSorter.Application.Queries.GetUsers;
using LoLTeamSorter.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorter.API.Endpoints
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
            })
                .WithName("GetUsers")
                .Produces<List<UserViewModel>>(StatusCodes.Status200OK);

            group.MapPost("/", [Authorize(Policy = "CreateUser")] async (CreateUserCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"users/{result}", result);
            });

            group.MapPut("/", [Authorize(Policy = "EditUser")] async (UpdateUserCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", [Authorize(Policy = "DeleteUser")] async (Guid id, ISender sender) =>
            {
                var command = new DeleteUserCommand(id);
                await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/delete", [Authorize(Policy = "DeleteUser")] async (DeleteUsersCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
