namespace Assessment.Domain.Entities;

public class Answer : BaseEntity
{
    public string Content { get; set; } = null!;
    public Guid QuestionId { get; set; }
}