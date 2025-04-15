using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Extensions
{
    public static class PlayerMapper
    {
        public static IEnumerable<PlayerViewModel> ToViewModelList(this IEnumerable<Player> players)
        {
            return players.Select(player => EntityToViewModel(player!));
        }

        public static PlayerViewModel ToViewModel(this Player player)
        {
            return EntityToViewModel(player);
        }

        private static PlayerViewModel EntityToViewModel(Player player)
        {
            return new PlayerViewModel
            (
                Id: player.Id.Value,
                RiotName: player.RiotIdentifier.Name,
                RiotTag: player.RiotIdentifier.Tag,
                MainLane: player.MainLane,
                SecondaryLane: player.SecondaryLane,
                Tier: player.RankedTier.Tier,
                Rank: player.RankedTier.Rank, 
                Stars: player.Stars,
                TierWeight: player.RankedTier.GetWeight()
            );
        }
    }
}
