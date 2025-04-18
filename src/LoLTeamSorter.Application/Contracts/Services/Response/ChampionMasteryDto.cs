using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionMasteryDto(
    [property: JsonPropertyName("championId")] int ChampionId,
    [property: JsonPropertyName("championLevel")] int ChampionLevel,
    [property: JsonPropertyName("championPoints")] int ChampionPoints)
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
