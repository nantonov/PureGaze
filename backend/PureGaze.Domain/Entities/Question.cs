namespace PureGaze.Domain.Entities;

public class Question : BaseEntity<int>
{
    public int SubTopicId { get; set; }
    public ICollection<QuestionTranslate> QuestionTranslates { get; set; } = [];
}