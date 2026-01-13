using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class AnswerTranslateConfiguration : IEntityTypeConfiguration<AnswerTranslate>
{
    public void Configure(EntityTypeBuilder<AnswerTranslate> builder)
    {
        builder.ToTable("AnswerTranslates");
        
        builder.HasKey(o => new { o.AnswerId, o.Language });

        builder.Property(o => o.Content)
            .HasMaxLength(4096);

        builder.HasOne<Answer>()
            .WithMany(o => o.AnswerTranslates)
            .HasForeignKey(o => o.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}