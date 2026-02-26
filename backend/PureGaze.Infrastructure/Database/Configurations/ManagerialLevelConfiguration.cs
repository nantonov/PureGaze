using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class ManagerialLevelConfiguration : IEntityTypeConfiguration<ManagerialLevel>
{
    public void Configure(EntityTypeBuilder<ManagerialLevel> builder)
    {
        builder.ToTable("ManagerialLevels");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Translation)
            .HasMaxLength(50);

        builder.Property(x => x.Value)
            .HasMaxLength(50);
    }
}