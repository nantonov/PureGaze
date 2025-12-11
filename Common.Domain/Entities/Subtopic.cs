namespace Common.Domain.Entities;

public class Subtopic : BaseDictionaryEntity
{
    public string Name { get; set; } = null!;
    public Guid TopicId { get; set; }
}