using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class SubtopicConfiguration : IEntityTypeConfiguration<Subtopic>
{
    public void Configure(EntityTypeBuilder<Subtopic> builder)
    {
        builder.ToTable("Subtopics");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasOne<Topic>()
            .WithMany() 
            .HasForeignKey(o => o.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
