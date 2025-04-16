using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.Commands.UpdatePlayer
{
    public record UpdatePlayerCommand(Guid Id, string Name, string RiotName, string RiotTag, LaneEnum MainLane,
        LaneEnum SecondaryLane, int Stars) : ICommand
    {
    }
}
