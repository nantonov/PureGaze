using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class SubtopicTranslateConfiguration : IEntityTypeConfiguration<SubtopicTranslate>
{
    public void Configure(EntityTypeBuilder<SubtopicTranslate> builder)
    {
        builder.ToTable("SubtopicTranslates");
        
        builder.HasKey(o => new { o.SubtopicId, o.Language });

        builder.Property(o => o.Name)
            .HasMaxLength(128);

        builder.HasOne<Subtopic>()
            .WithMany(o => o.SubtopicTranslates)
            .HasForeignKey(o => o.SubtopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}