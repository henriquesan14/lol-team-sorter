using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class PermissionExtensions
    {
        public static IEnumerable<PermissionViewModel> ToViewModelList(this IEnumerable<Permission> permissions)
        {
            return permissions.Select(permission => EntityToViewModel(permission!));
        }

        public static PermissionViewModel ToViewModel(this Permission permission)
        {
            return EntityToViewModel(permission);
        }

        private static PermissionViewModel EntityToViewModel(Permission permission)
        {
            return new PermissionViewModel
            (
                Name: permission.Name
            );
        }
    }
}
