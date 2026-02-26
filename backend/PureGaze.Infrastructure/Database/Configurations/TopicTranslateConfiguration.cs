using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class TopicTranslateConfiguration : IEntityTypeConfiguration<TopicTranslate>
{
    public void Configure(EntityTypeBuilder<TopicTranslate> builder)
    {
        builder.ToTable("TopicTranslates");

        builder.HasKey(o => new { o.TopicId, o.Language });

        builder.Property(o => o.Name)
            .HasMaxLength(128);

        builder.HasOne<Topic>()
            .WithMany(o => o.TopicTranslates)
            .HasForeignKey(o => o.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}