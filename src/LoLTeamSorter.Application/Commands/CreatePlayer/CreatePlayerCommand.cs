using LoLTeamSorter.Domain.Enums;
using MediatR;

namespace LoLTeamSorter.Application.Commands.CreatePlayer
{
    public record CreatePlayerCommand(string Name, string RiotName, string RiotTag, LaneEnum MainLane,
        LaneEnum SecondaryLane, TierEnum Tier, RankEnum? Rank, int Stars) : IRequest<Guid>;
}
