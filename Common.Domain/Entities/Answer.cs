namespace Common.Domain.Entities;

public class Answer : BaseDictionaryEntity
{
    public string Content { get; set; } = null!;
    public Guid QuestionId { get; set; }
}