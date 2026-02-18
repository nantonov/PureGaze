using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class SubtopicTranslate
{
    public int SubtopicId { get; set; }
    public Language Language { get; set; }
    public string? Name { get; set; }
}