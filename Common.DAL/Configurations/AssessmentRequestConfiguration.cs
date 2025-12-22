using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class AssessmentRequestConfiguration : IEntityTypeConfiguration<AssessmentRequest>
{
    public void Configure(EntityTypeBuilder<AssessmentRequest> builder)
    {
        builder.ToTable("AssessmentRequests");
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.HasOne(o => o.Candidate)
            .WithMany()
            .HasForeignKey(o => o.CandidateId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.AssignedM1)
            .WithMany()
            .HasForeignKey(o => o.AssignedM1Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.RejectionReason)
            .HasMaxLength(1000);

        builder.HasIndex(o => o.Status);
    }
}