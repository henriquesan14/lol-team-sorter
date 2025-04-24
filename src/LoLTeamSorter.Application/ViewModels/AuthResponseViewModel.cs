namespace LoLTeamSorter.Application.ViewModels
{
    public record AuthResponseViewModel(string AccessToken, UserViewModel User)
    {
        public string? RedirectAppUrl { get; set; }
    }
}
