namespace LoLTeamSorter.Application.ViewModels
{
    public record AuthResponseViewModel(string AccessToken, UserViewModel User, string? RedirectAppUrl);
}
