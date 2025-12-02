namespace Assessment.Domain.Entities;

public class Topic : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid TemplateId { get; set; }
}