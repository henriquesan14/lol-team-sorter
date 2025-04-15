namespace LoLTeamSorter.Application.ViewModels
{
    public record TeamViewModel(List<PlayerViewModel> Players, int TotalStars, double AverageTierWeight);
}
