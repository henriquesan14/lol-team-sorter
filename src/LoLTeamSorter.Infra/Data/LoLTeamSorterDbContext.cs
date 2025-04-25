using LoLTeamSorter.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LoLTeamSorter.Infra.Data
{
    public class LoLTeamSorterDbContext : DbContext, ILoLTeamSorterDbContext
    {
        public LoLTeamSorterDbContext(DbContextOptions<LoLTeamSorterDbContext> options)
        : base(options) { }

        public DbSet<Player> Players => Set<Player>();

        public DbSet<Team> Teams => Set<Team>();

        public DbSet<Matchmaking> Matchmakings => Set<Matchmaking>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
