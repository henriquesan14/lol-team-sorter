namespace LoLTeamSorter.Application.ViewModels
{
    public record AuthResponseViewModel(string AccessToken, string RefreshToken,
    DateTime RefreshTokenExpiresAt, UserViewModel User, string? RedirectAppUrl);
}
