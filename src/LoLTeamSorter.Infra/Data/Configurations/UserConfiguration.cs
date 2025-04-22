using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoLTeamSorter.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Of(value)
                );

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Username)
                .HasConversion(
                    email => email.Value,
                    email => Username.Of(email)
                 )
                .IsRequired()
                .HasMaxLength(30);
            builder
                .HasIndex(e => e.Username)
                .IsUnique();

            builder.Property(d => d.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Group)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
