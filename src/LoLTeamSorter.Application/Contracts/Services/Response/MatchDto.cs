namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record MatchDto(MetadataDto Metadata, InfoDto Info);

    public record MetadataDto(List<string> Participants);

    public record InfoDto(List<ParticipantDto> Participants, int QueueId, int GameDuration);

    public record ParticipantDto(string Puuid, int ChampionId, string ChampionName, bool Win, int Kills, int Deaths, int Assists);
}
