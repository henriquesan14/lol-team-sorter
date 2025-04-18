using LoLTeamSorter.Application.Contracts.Services.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IRiotMatchApi
    {
        [Get("/lol/match/v5/matches/by-puuid/{puuid}/ids")]
        Task<List<string>> GetMatchIdsAsync(string puuid, [Query] int count = 20, [Query] int queue = 420); // 420 = ranked solo

        [Get("/lol/match/v5/matches/{matchId}")]
        Task<MatchDto> GetMatchAsync(string matchId);
    }
}
