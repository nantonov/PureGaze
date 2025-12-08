using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class ProfessionalLevelConfiguration : IEntityTypeConfiguration<ProfessionalLevel>
{
    public void Configure(EntityTypeBuilder<ProfessionalLevel> builder)
    {
        builder.ToTable("ProfessionalLevels");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
     
        builder.Property(x => x.Translation).HasMaxLength(50);
        builder.Property(x => x.Value).HasMaxLength(50);
    }
}