using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class SkillLevelConfiguration : IEntityTypeConfiguration<SkillLevel>
{
    public void Configure(EntityTypeBuilder<SkillLevel> builder)
    {
        builder.ToTable("SkillLevels");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
     
        builder.Property(x => x.Translation).HasMaxLength(50);
        builder.Property(x => x.Value).HasMaxLength(50);
    }
}