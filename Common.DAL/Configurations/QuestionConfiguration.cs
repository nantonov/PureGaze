using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.SubTopicId)
            .IsRequired();
        
        builder.HasOne<Subtopic>()
            .WithMany() 
            .HasForeignKey(o => o.SubTopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
