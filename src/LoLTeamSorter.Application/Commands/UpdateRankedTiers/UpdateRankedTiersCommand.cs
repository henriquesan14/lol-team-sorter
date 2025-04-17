using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.UpdateRankedTiers
{
    public record UpdateRankedTiersCommand(List<Guid> PlayerIds) : ICommand
    {
    }
}
