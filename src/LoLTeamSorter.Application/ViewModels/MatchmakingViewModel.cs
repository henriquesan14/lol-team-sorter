using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.ViewModels
{
    public record MatchmakingViewModel(Guid Id, ModeEnum Mode, TeamViewModel BlueTeam, TeamViewModel RedTeam, DateTime CreatedAt);
}
