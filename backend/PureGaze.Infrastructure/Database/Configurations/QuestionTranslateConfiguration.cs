using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class QuestionTranslateConfiguration : IEntityTypeConfiguration<QuestionTranslate>
{
    public void Configure(EntityTypeBuilder<QuestionTranslate> builder)
    {
        builder.ToTable("QuestionTranslates");

        builder.HasKey(o => new { o.QuestionId, o.Language });

        builder.Property(o => o.Content)
            .HasMaxLength(512);

        builder.HasOne<Question>()
            .WithMany(o => o.QuestionTranslates)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}