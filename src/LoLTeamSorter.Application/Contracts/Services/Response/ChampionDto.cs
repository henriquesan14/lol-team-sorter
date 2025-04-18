namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionDto(
        string Key,
        string Name,
        ImageDto Image);
}
