using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionDto(
        [property: JsonPropertyName("key")] string Key,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("image")] ImageDto Image );
}
