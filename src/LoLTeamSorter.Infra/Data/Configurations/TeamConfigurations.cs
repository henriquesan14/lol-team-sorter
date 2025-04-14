using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => TeamId.Of(value)
                );

            builder.Property(t => t.Name)
                .IsRequired();

            builder.HasMany(t => t.Players)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
