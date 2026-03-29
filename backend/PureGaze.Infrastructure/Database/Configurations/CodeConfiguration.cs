using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class CodeConfiguration : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
    {
        builder.ToTable("Codes");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Name)
            .HasMaxLength(128);

        builder.HasOne(o => o.Grade)
            .WithMany()
            .HasForeignKey(o => o.GradeId);

        builder.HasOne(o => o.ToGrade)
            .WithMany()
            .HasForeignKey(o => o.ToGradeId);
    }
}
