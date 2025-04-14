using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PlayerId.Of(value)
                );

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.MainLane)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.SecondaryLane)
                .HasConversion<string>()
                .IsRequired();

            builder.OwnsOne(p => p.RankedTier, rt =>
            {
                rt.Property(x => x.Tier)
                    .HasConversion<string>()
                    .HasColumnName("Tier")
                    .IsRequired();

                rt.Property(x => x.Rank)
                    .HasConversion<string>()
                    .HasColumnName("Rank");
            });

            builder.Property(p => p.Stars)
                .IsRequired();

            builder.Property(p => p.RiotId)
                .IsRequired();
        }
    }
}
