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

        builder.Property(x => x.Subject)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Body)
            .IsRequired();
        
        builder.Property(x => x.To)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.From)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(500);
        
        builder.HasIndex(x => new { x.Status, x.Priority });
    }
}
