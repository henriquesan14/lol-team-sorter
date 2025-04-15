using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.ViewModels
{
    public record MatchmakingViewModel(ModeEnum Mode, TeamViewModel BlueTeam, TeamViewModel RedTeam, DateTime CreatedAt);
}
