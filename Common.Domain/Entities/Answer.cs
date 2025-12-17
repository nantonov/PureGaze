namespace Common.Domain.Entities;

public class Answer : BaseEntity<int>
{
    public int QuestionId { get; set; }
    public virtual ICollection<AnswerTranslate> AnswerTranslates { get; set; } = [];
}