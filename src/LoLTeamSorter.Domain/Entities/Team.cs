using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Team : Aggregate<TeamId>
    {
        public string Name { get; private set; } = default!;
        public List<Player> Players { get; private set; } = new();

        public int TotalStars => Players.Sum(p => p.Stars);

        public double AverageTierWeight => Players.Any()
            ? Players.Average(p => p.RankedTier.GetWeight())
            : 0;

    }
}
