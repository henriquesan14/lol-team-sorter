using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Player : Aggregate<PlayerId>
    {
        public string Name { get; private set; } = default!;
        public LaneEnum MainLane { get; private set; } = default!;
        public LaneEnum SecondaryLane { get; private set; } = default!;
        public RankedTier RankedTier { get; private set; } = default!;
        public int Stars { get; private set; } = default!;
        public string RiotId { get; private set; } = default!;
    }
}
