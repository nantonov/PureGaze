using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class CodeTranslate
{
    public int CodeId { get; set; }
    public Language Language { get; set; }
    public string? LevelVision { get; set; }
}