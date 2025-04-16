using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Player : Aggregate<PlayerId>
    {
        public string Name { get; private set; } = default!;
        public RiotIdentifier RiotIdentifier { get; private set; } = default!;
        public LaneEnum MainLane { get; private set; } = default!;
        public LaneEnum SecondaryLane { get; private set; } = default!;
        public RankedTier RankedTier { get; private set; } = default!;
        public int Stars { get; private set; } = default!;
        public string RiotId { get; private set; } = default!;

        public List<Team> Teams { get; private set; } = new();

        public static Player Create(PlayerId id, string name, RiotIdentifier riotIdentifier, LaneEnum mainLane,
            LaneEnum secondaryLane, RankedTier rankedTier, int stars, string riotId)
        {
            return new Player {
                Id= id,
                Name= name,
                RiotIdentifier= riotIdentifier,
                MainLane= mainLane,
                SecondaryLane= secondaryLane,
                RankedTier= rankedTier,
                Stars= stars,
                RiotId= riotId
            };
        }

        public void Update(string name, LaneEnum mainLane, LaneEnum secondaryLane, int stars)
        {
            Name = name;
            MainLane = mainLane;
            SecondaryLane = secondaryLane;
            Stars = stars;
        }

        public void SetRankedTier(RankedTier rankedTier)
        {
            RankedTier = rankedTier;
        }

        public void SetRiotIdentifier(RiotIdentifier riotIdentifier)
        {
            RiotIdentifier = riotIdentifier;
        }

        public void SetRiotId(string riotId)
        {
            RiotId = riotId;
        }
    }
}
