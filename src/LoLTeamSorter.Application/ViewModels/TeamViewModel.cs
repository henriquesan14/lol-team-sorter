namespace LoLTeamSorter.Application.ViewModels
{
    public record TeamViewModel(Guid Id, List<PlayerViewModel> Players, int TotalStars, double AverageTierWeight);
}
