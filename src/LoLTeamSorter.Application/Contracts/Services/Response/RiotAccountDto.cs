using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Response
{
    public record RiotAccountDto(
        [property: JsonPropertyName("puuid")] string Puuid, 
        [property: JsonPropertyName("gameName")] string GameName, 
        [property: JsonPropertyName("tagLine")] string TagLine);
}
