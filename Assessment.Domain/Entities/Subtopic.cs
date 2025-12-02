namespace Assessment.Domain.Entities;

public class Subtopic : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid TopicId { get; set; }
}