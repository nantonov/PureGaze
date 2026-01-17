namespace PureGaze.Domain.Entities;

public class Answer : BaseEntity<int>
{
    public int QuestionId { get; set; }
    public ICollection<AnswerTranslate> AnswerTranslates { get; set; } = [];
}