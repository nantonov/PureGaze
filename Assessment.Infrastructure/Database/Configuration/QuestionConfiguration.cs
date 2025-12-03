using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Infrastructure.Database.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(o => o.SubTopicId)
            .IsRequired();

        builder.HasOne<Subtopic>()
            .WithMany() 
            .HasForeignKey(o => o.SubTopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.CreatedAt)
            .IsRequired();  
    }
}
