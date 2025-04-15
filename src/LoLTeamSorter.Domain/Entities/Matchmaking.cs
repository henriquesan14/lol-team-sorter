using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Matchmaking : Aggregate<MatchmakingId>
    {
        public Team BlueTeam { get; private set; } = default!;
        public Team RedTeam { get; private set; } = default!;
        public ModeEnum Mode { get; private set; } = default!;

        public static Matchmaking Create(MatchmakingId id, ModeEnum mode, Team blueTeam, Team redTeam)
        {
            return new Matchmaking {
                Id = id,
                Mode = mode,
                BlueTeam = blueTeam,
                RedTeam = redTeam
            };
        }
    }
}
