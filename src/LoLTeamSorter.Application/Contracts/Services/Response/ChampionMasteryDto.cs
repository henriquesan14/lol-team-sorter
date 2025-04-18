namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionMasteryDto(
    int ChampionId,
    int ChampionLevel,
    int ChampionPoints)
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
