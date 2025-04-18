using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class MatchmakingExtensions
    {
        public static IEnumerable<MatchmakingViewModel> ToViewModelList(this IEnumerable<Matchmaking> matchmakings)
        {
            return matchmakings.Select(matchmaking => EntityToViewModel(matchmaking!));
        }

        public static MatchmakingViewModel ToViewModel(this Matchmaking matchmaking)
        {
            return EntityToViewModel(matchmaking);
        }

        private static MatchmakingViewModel EntityToViewModel(Matchmaking matchmaking)
        {
            return new MatchmakingViewModel
            (
                Id: matchmaking.Id.Value,
                BlueTeam: matchmaking.BlueTeam.ToViewModel(),
                RedTeam: matchmaking.RedTeam.ToViewModel(),
                Mode: matchmaking.Mode,
                CreatedAt: matchmaking.CreatedAt!.Value
            );
        }
    }
}
