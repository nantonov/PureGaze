using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PureGaze.Infrastructure.Database.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasOne(o => o.Subtopic)
            .WithMany(o => o.Questions) 
            .HasForeignKey(o => o.SubTopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
