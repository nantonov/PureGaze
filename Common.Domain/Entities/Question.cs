namespace Common.Domain.Entities;

public class Question : BaseEntity<int>
{
    public int SubTopicId { get; set; }
    public virtual ICollection<QuestionTranslate> QuestionTranslates { get; set; } = [];
}