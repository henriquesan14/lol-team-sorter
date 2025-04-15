using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class MatchmakingExtensions
    {
        public static IEnumerable<MatchmakingViewModel> ToViewModelList(this IEnumerable<Matchmaking> teams)
        {
            return teams.Select(team => EntityToViewModel(team!));
        }

        public static MatchmakingViewModel ToViewModel(this Matchmaking team)
        {
            return EntityToViewModel(team);
        }

        private static MatchmakingViewModel EntityToViewModel(Matchmaking team)
        {
            return new MatchmakingViewModel
            (
                BlueTeam: team.BlueTeam.ToViewModel(),
                RedTeam: team.RedTeam.ToViewModel(),
                Mode: team.Mode,
                CreatedAt: team.CreatedAt!.Value
            );
        }
    }
}
