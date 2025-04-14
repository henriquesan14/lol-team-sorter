using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class MatchmakingConfiguration : IEntityTypeConfiguration<Matchmaking>
    {
        public void Configure(EntityTypeBuilder<Matchmaking> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => MatchmakingId.Of(value)
                );

            builder.Property(m => m.Mode)
                .IsRequired();

            builder.HasMany(typeof(Player), "Players");

            builder.HasOne(m => m.BlueTeam)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.RedTeam)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
