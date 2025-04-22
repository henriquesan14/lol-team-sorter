using Carter;
using LoLTeamSorter.Application.Queries.GetPermissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LoLTeamSorte.API.Endpoints
{
    public class PermissionEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/permissions");

            group.MapGet("/", [Authorize(Policy = "CreateUser")] async (ISender sender) =>
            {
                var query = new GetPermissionsQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
