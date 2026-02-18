using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class QuestionTranslate
{
    public int QuestionId { get; set; }
    public Language Language { get; set; }
    public string? Content { get; set; }
}