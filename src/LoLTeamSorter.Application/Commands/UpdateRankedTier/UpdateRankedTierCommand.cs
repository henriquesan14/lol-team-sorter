using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.UpdateRankedTier
{
    public record UpdateRankedTierCommand(Guid Id) : ICommand
    {
    }
}
