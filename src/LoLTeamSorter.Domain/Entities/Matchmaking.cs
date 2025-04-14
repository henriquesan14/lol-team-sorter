using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Matchmaking : Aggregate<MatchmakingId>
    {
        public IReadOnlyList<Player> Players { get; private set; }
        public Team BlueTeam { get; private set; } = default!;
        public Team RedTeam { get; private set; } = default!;
        public string Mode { get; private set; } = default!;
    }
}
