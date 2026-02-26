using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FirstNameEn)
            .HasMaxLength(50);

        builder.Property(x => x.LastNameEn)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasMaxLength(100);

        builder.HasOne(x => x.Manager)
            .WithMany()
            .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Head)
            .WithMany()
            .HasForeignKey(x => x.HeadId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.RM)
            .WithMany()
            .HasForeignKey(x => x.RMId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.M1)
            .WithMany()
            .HasForeignKey(x => x.M1Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.M2)
            .WithMany()
            .HasForeignKey(x => x.M2Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.M3)
            .WithMany()
            .HasForeignKey(x => x.M3Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.M4)
            .WithMany()
            .HasForeignKey(x => x.M4Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}