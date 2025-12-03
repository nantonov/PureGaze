using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Infrastructure.Database.Configuration;

public class CodeConfiguration : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
    {
        builder.ToTable("Codes");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.GradeId)
            .IsRequired();

        builder.Property(o => o.ToGradeId)
            .IsRequired();

        builder.Property(o => o.Display)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(o => o.LevelVision)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(o => o.TotalEx)
            .IsRequired();

        builder.Property(o => o.DiffEx)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .IsRequired();  
    }
}
