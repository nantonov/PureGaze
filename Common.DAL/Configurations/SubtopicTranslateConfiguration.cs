using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class SubtopicTranslateConfiguration : IEntityTypeConfiguration<SubtopicTranslate>
{
    public void Configure(EntityTypeBuilder<SubtopicTranslate> builder)
    {
        builder.ToTable("SubtopicTranslates");
        builder.HasKey(o => new { o.SubtopicId, o.Language });

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(o => o.Language)
            .IsRequired();

        builder.HasOne<Subtopic>()
            .WithMany(o => o.SubtopicTranslates)
            .HasForeignKey(o => o.SubtopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}