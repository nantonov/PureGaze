using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Infrastructure.Database.Configuration;

public class SubtopicConfiguration : IEntityTypeConfiguration<Subtopic>
{
    public void Configure(EntityTypeBuilder<Subtopic> builder)
    {
        builder.ToTable("Subtopics");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(o => o.TopicId)
            .IsRequired();

        builder.HasOne<Topic>()
            .WithMany() 
            .HasForeignKey(o => o.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.CreatedAt)
            .IsRequired();  
    }
}
