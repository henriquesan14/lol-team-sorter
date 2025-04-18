using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Response
{
    public record RiotLeagueEntryDto(
        string QueueType, 
        string Tier, 
        string Rank, 
        int LeaguePoints, 
        int Wins, 
        int Losses);
}
