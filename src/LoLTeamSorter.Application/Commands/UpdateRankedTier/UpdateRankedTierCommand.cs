using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdateRankedTier
{
    public record UpdateRankedTierCommand(Guid Id) : IRequest<Unit>
    {
    }
}
