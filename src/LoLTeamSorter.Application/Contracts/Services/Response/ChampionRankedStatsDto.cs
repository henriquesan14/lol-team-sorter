namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionRankedStatsDto(int ChampionId)
    {
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WinRate => Matches > 0
        ? Math.Round((double)Wins / Matches * 100, 2)
        : 0;
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
