using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.Commands.CreatePlayer
{
    public record CreatePlayerCommand(string Name, string RiotName, string RiotTag, LaneEnum MainLane,
        LaneEnum SecondaryLane, int Stars) : ICommand<Guid>;
}
