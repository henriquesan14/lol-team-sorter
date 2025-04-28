using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.ViewModels
{
    public record PlayerViewModel(Guid Id, string Name, string RiotName, string RiotTag, LaneEnum MainLane,
        LaneEnum SecondaryLane, TierEnum Tier, RankEnum? Rank, int Stars, int TierWeight, string RiotId, int Victories);
}
