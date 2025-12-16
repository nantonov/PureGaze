namespace Common.Domain.Entities;

public class Answer : BaseEntity<int>
{
    public ICollection<AnswerTranslate> AnswerTranslates { get; set; } = null!;
    public int QuestionId { get; set; }
}