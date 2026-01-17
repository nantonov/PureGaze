using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class CodeTranslateConfiguration : IEntityTypeConfiguration<CodeTranslate>
{
    public void Configure(EntityTypeBuilder<CodeTranslate> builder)
    {
        builder.ToTable("CodeTranslates");
        
        builder.HasKey(o => new { o.CodeId, o.Language });

        builder.Property(o => o.LevelVision)
            .HasMaxLength(512);

        builder.HasOne<Code>()
            .WithMany(o => o.CodeTranslates)
            .HasForeignKey(o => o.CodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}