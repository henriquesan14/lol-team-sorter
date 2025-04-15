using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Enums;
using MediatR;

namespace LoLTeamSorter.Application.Commands.GenerateMatchmaking
{
    public record GenerateMatchmakingCommand(ModeEnum Mode, List<Guid> PlayerIds) : IRequest<MatchmakingViewModel>;
}
