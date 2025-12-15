namespace Common.Domain.Entities;

public class Question : BaseEntity<int>
{
    public ICollection<QuestionTranslate> QuestionTranslates { get; set; } = null!;
    public Guid SubTopicId { get; set; }
}