using Common.Data.Enums;

namespace Common.Domain.Entities;

public class QuestionTranslate
{
    public int QuestionId { get; set; }
    public Language Language { get; set; }
    public string Content { get; set; } = null!;
}