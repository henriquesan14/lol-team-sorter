using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Services
{
    public static class MatchmakingLogic
    {
        public static (List<Player> BlueTeam, List<Player> RedTeam) BalanceByStars(List<Player> players)
        {
            var sortedPlayers = players.OrderByDescending(p => p.Stars).ToList();
            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            for (int i = 0; i < sortedPlayers.Count; i++)
            {
                var player = sortedPlayers[i];

                var blueStars = blueTeam.Sum(p => p.Stars);
                var redStars = redTeam.Sum(p => p.Stars);

                if (blueStars <= redStars)
                    blueTeam.Add(player);
                else
                    redTeam.Add(player);
            }

            return (blueTeam, redTeam);
        }

        public static (List<Player> BlueTeam, List<Player> RedTeam) BalanceByTier(List<Player> players)
        {
            var sortedPlayers = players.OrderByDescending(p => p.RankedTier.GetWeight()).ToList();
            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            for (int i = 0; i < sortedPlayers.Count; i++)
            {
                var player = sortedPlayers[i];

                var blueWeight = blueTeam.Sum(p => p.RankedTier.GetWeight());
                var redWeight = redTeam.Sum(p => p.RankedTier.GetWeight());

                if (blueWeight <= redWeight)
                    blueTeam.Add(player);
                else
                    redTeam.Add(player);
            }

            return (blueTeam, redTeam);
        }
    }
}
