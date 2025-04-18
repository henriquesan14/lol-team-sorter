using System.Text.Json.Serialization;

namespace LoLTeamSorter.Application.Contracts.Response
{
    public record RiotAccountDto(
        string Puuid, 
        string GameName, 
        string TagLine);
}
