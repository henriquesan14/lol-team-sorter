using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetPermissions
{
    public record GetPermissionsQuery : IQuery<List<PermissionByCategoryViewModel>>
    {
    }
}
