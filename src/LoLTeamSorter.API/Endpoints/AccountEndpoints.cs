using Carter;
using LoLTeamSorter.Application.Commands.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorter.API.Endpoints
{
    public class AccountEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/account");

            group.MapPut("/", [Authorize] async (UpdatePasswordCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
