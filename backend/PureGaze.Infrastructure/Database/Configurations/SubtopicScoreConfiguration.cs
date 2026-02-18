using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Configurations;

public class SubtopicScoreConfiguration : IEntityTypeConfiguration<SubtopicScore>
{
    public void Configure(EntityTypeBuilder<SubtopicScore> builder)
    {
        builder.ToTable("SubtopicScores");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Comment)
            .HasMaxLength(2000);

        builder.HasOne(x => x.Stage)
            .WithMany(x => x.Scores);

        builder.HasOne(x => x.Subtopic)
            .WithMany()
            .HasForeignKey(x => x.SubtopicId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}