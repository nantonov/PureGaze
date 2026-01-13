using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Configurations;

public class QuestionScoreConfiguration : IEntityTypeConfiguration<QuestionScore>
{
    public void Configure(EntityTypeBuilder<QuestionScore> builder)
    {
        builder.ToTable("QuestionScores");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Comment)
            .HasMaxLength(2000);

        builder.HasOne(x => x.Stage)
            .WithMany(x => x.Scores)
            .IsRequired();

        builder.HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}