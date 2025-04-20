using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => GroupId.Of(dbId));
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(p => p.Permissions)
                .WithMany(a => a.Groups)
                .UsingEntity(p => p.ToTable("PermissionsGroup"));

            builder.HasMany(d => d.Users)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId);
        }
    }
}
