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

        builder.HasOne(o => o.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Manager)
            .WithMany()
            .HasForeignKey(o => o.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Code)
            .WithMany()
            .HasForeignKey(o => o.CodeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.RejectionReason)
            .HasMaxLength(1000);

        builder.HasIndex(o => o.Status);
    }
}