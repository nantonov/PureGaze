using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable("Emails");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Subject)
            .HasMaxLength(200);

        builder.Property(x => x.To)
            .HasMaxLength(200);

        builder.Property(x => x.From)
            .HasMaxLength(200);
    }
}
