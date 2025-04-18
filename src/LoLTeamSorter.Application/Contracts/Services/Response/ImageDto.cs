using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ImageDto(
        [property: JsonPropertyName("full")] string Full)
    {
    }
}
