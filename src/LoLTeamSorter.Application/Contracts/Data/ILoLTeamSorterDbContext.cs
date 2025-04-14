using LoLTeamSorter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Contracts.Data
{
    public interface ILoLTeamSorterDbContext
    {
        DbSet<Player> Players { get; }
        DbSet<Team> Teams { get; }
        DbSet<Matchmaking> Matchmakings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
