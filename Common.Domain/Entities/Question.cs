namespace Common.Domain.Entities;

public class Question : BaseDictionaryEntity
{
    public string Content { get; set; } = null!;
    public Guid SubTopicId { get; set; }
}