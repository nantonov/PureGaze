using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.CodeId)
            .IsRequired();
        
        builder.HasOne<Code>()
            .WithOne() 
            .HasForeignKey<Template>(o => o.CodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
