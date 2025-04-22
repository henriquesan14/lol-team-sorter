using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => PermissionId.Of(dbId));
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasMany(p => p.Groups)
                .WithMany(a => a.Permissions)
                .UsingEntity(p => p.ToTable("PermissionsGroup"));

            builder.Property(d => d.PermissionCategory)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
