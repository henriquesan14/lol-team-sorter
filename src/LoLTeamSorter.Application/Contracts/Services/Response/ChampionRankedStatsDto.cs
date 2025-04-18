namespace LoLTeamSorter.Application.Contracts.Services.Response
{
    public record ChampionRankedStatsDto(int ChampionId)
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WinRate => Matches > 0
        ? Math.Round((double)Wins / Matches * 100, 2)
        : 0;

        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalAssists { get; set; }

        public double AvgKills => Matches > 0 ? Math.Round((double)TotalKills / Matches, 1) : 0;
        public double AvgDeaths => Matches > 0 ? Math.Round((double)TotalDeaths / Matches, 1) : 0;
        public double AvgAssists => Matches > 0 ? Math.Round((double)TotalAssists / Matches, 1) : 0;

        public double Kda => Math.Round((double)(TotalKills + TotalAssists) / Math.Max(1, TotalDeaths), 2);
    }
}
