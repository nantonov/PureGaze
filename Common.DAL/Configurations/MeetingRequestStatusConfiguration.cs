using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class MeetingRequestStatusConfiguration : IEntityTypeConfiguration<MeetingRequestStatus>
{
    public void Configure(EntityTypeBuilder<MeetingRequestStatus> builder)
    {
        builder.ToTable("MeetingRequestStatuses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
     
        builder.Property(x => x.Translation).HasMaxLength(50);
        builder.Property(x => x.Value).HasMaxLength(50);
    }
}