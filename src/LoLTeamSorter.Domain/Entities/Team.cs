using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Team : Aggregate<TeamId>
    {
        public List<Player> Players { get; private set; } = new();

        public int TotalStars => Players.Sum(p => p.Stars);

        public double AverageTierWeight => Players.Any()
            ? Players.Average(p => p.RankedTier.GetWeight())
            : 0;

        public static Team Create(TeamId id, List<Player> players)
        {
            return new Team
            {
                Id = id,
                Players = players
            };
        }

    }
}
