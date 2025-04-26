namespace LoLTeamSorter.Application.Contracts.Services
{
    public interface ITokenCleanupService
    {
        Task CleanupExpiredAndRevokedTokensAsync();
    }
}
