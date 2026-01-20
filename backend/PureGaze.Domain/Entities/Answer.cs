namespace PureGaze.Domain.Entities;

public class Answer : BaseEntity<int>
{
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public ICollection<AnswerTranslate> AnswerTranslates { get; set; } = [];
}