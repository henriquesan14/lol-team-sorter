using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class GroupExtensions
    {
        public static IEnumerable<GroupViewModel> ToViewModelList(this IEnumerable<Group> groups)
        {
            return groups.Select(group => EntityToViewModel(group!));
        }

        public static GroupViewModel ToViewModel(this Group group)
        {
            return EntityToViewModel(group);
        }

        private static GroupViewModel EntityToViewModel(Group group)
        {
            return new GroupViewModel
            (
                Id:group.Id.Value,
                Name: group.Name,
                Permissions: group.Permissions.ToViewModelList().ToList()
            );
        }
    }
}
