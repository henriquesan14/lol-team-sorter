using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.Commands.GenerateMatchmaking
{
    public record GenerateMatchmakingCommand(ModeEnum Mode, List<Guid> PlayerIds) : ICommand<MatchmakingViewModel>;
}
