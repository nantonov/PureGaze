using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class CodeTranslateConfiguration : IEntityTypeConfiguration<CodeTranslate>
{
    public void Configure(EntityTypeBuilder<CodeTranslate> builder)
    {
        builder.ToTable("CodeTranslates");
        
        builder.HasKey(o => new { o.CodeId, o.Language });

        builder.Property(o => o.LevelVision)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(o => o.Language)
            .IsRequired();

        builder.HasOne<Code>()
            .WithMany(o => o.CodeTranslates)
            .HasForeignKey(o => o.CodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}