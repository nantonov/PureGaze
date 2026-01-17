using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class AnswerTranslate
{
    public int AnswerId { get; set; }
    public Language Language { get; set; }
    public string Content { get; set; } = null!;
}