using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Response
{
    public record RiotLeagueEntryDto(
        [property: JsonPropertyName("queueType")] string QueueType, 
        [property: JsonPropertyName("tier")] string Tier, 
        [property: JsonPropertyName("rank")] string Rank, 
        [property: JsonPropertyName("leaguePoints")] int LeaguePoints, 
        [property: JsonPropertyName("wins")] int Wins, 
        [property: JsonPropertyName("losses")] int Losses);
}
