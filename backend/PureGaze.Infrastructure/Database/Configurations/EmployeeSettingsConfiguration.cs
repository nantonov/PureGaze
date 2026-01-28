using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Configurations;

public class EmployeeSettingsConfiguration : IEntityTypeConfiguration<EmployeeSettings>
{
    public void Configure(EntityTypeBuilder<EmployeeSettings> builder)
    {
        builder.ToTable("EmployeeSettings");

        builder.HasKey(x => x.EmployeeId);
        
        builder.HasOne(x => x.Employee)
            .WithOne(x => x.EmployeeSettings)
            .HasForeignKey<EmployeeSettings>(x => x.EmployeeId);
    }
}