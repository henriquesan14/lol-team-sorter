using LoLTeamSorter.Domain.Enums;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdatePlayer
{
    public record UpdatePlayerCommand(Guid Id, string Name, string RiotName, string RiotTag, LaneEnum MainLane,
        LaneEnum SecondaryLane, int Stars) : IRequest<Unit>
    {
    }
}
