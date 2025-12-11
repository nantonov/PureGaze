using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable("Emails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Subject).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.To).HasMaxLength(200).IsRequired();
        builder.Property(x => x.From).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Cc).HasMaxLength(500);
        builder.Property(x => x.Bcc).HasMaxLength(500);
        
        builder.Property(x => x.ErrorMessage).HasMaxLength(500);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Priority)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        //retries (error message is not null) will be made based on priority  
        builder.HasIndex(x => new { x.ErrorMessage, x.Priority });
        builder.HasIndex(x => x.EmployeeId);
    }
}
