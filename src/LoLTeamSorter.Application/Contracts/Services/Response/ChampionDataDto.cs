using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionDataDto([property: JsonPropertyName("data")] Dictionary<string, ChampionDto> Data);
}
