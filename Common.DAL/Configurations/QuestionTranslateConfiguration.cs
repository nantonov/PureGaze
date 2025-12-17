using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class QuestionTranslateConfiguration : IEntityTypeConfiguration<QuestionTranslate>
{
    public void Configure(EntityTypeBuilder<QuestionTranslate> builder)
    {
        builder.ToTable("QuestionTranslates");

        builder.HasKey(o => new { o.QuestionId, o.Language });

        builder.Property(o => o.Content)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(o => o.Language)
            .IsRequired();

        builder.HasOne<Question>()
            .WithMany(o => o.QuestionTranslates)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}