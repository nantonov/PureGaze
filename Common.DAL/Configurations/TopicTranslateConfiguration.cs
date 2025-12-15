using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class TopicTranslateConfiguration : IEntityTypeConfiguration<TopicTranslate>
{
    public void Configure(EntityTypeBuilder<TopicTranslate> builder)
    {
        builder.ToTable("TopicTranslates");
        builder.HasKey(o => new { o.TopicId, o.Language });

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(o => o.Language)
            .IsRequired();

        builder.HasOne<Topic>()
            .WithMany(o => o.TopicTranslates)
            .HasForeignKey(o => o.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}