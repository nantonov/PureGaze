namespace Common.Domain.Entities;

public class Question : BaseEntity<int>
{
    public ICollection<QuestionTranslate> QuestionTranslates { get; set; } = null!;
    public int SubTopicId { get; set; }
}