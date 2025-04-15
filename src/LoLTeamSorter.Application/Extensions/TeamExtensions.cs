using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class TeamExtensions
    {
        public static IEnumerable<TeamViewModel> ToViewModelList(this IEnumerable<Team> teams)
        {
            return teams.Select(team => EntityToViewModel(team!));
        }

        public static TeamViewModel ToViewModel(this Team team)
        {
            return EntityToViewModel(team);
        }

        private static TeamViewModel EntityToViewModel(Team team)
        {
            return new TeamViewModel
            (
                Players: team.Players.ToViewModelList().ToList(),
                TotalStars: team.TotalStars,
                AverageTierWeight: team.AverageTierWeight
            );
        }
    }
}
