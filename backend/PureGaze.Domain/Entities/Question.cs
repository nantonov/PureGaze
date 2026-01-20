namespace PureGaze.Domain.Entities;

public class Question : BaseEntity<int>
{
    public int SubTopicId { get; set; }
    public Subtopic Subtopic { get; set; } = null!;
    public ICollection<QuestionTranslate> QuestionTranslates { get; set; } = [];
    public Answer Answer { get; set; } = null!;
}