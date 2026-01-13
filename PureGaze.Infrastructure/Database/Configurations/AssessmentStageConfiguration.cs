using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Configurations;

public class AssessmentStageConfiguration : IEntityTypeConfiguration<AssessmentStage>
{
    public void Configure(EntityTypeBuilder<AssessmentStage> builder)
    {
        builder.ToTable("AssessmentStages");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.StageType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Summary)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Assessment)
            .WithMany(x => x.Stages)
            .IsRequired();

        builder.HasMany(x => x.Scores)
            .WithOne(x => x.Stage)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Topic)
            .WithMany()
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Assessor)
            .WithMany()
            .HasForeignKey(x => x.AssessorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}