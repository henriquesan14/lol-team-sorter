using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IDDragonVersionApi
    {
        [Get("/api/versions.json")]
        Task<List<string>> GetVersionsAsync();
    }
}
