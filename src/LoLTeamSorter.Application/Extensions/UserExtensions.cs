using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class UserExtensions
    {
        public static IEnumerable<UserViewModel> ToViewModelList(this IEnumerable<User> users)
        {
            return users.Select(user => EntityToViewModel(user!));
        }

        public static UserViewModel ToViewModel(this User user)
        {
            return EntityToViewModel(user);
        }

        private static UserViewModel EntityToViewModel(User user)
        {
            return new UserViewModel
            (
                Id: user.Id.Value,
                Name: user.Name,
                Username: user.Username.Value,
                Group: user.Group.ToViewModel(),
                AvatarUrl: user.AvatarUrl
            );
        }
    }
}
