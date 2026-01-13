using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class QuestionScore : BaseEntity<Guid>
{
    public AssessmentStage Stage { get; set; } = null!;
    public Guid QuestionTemplateId { get; set; }
    public AssessmentMark Score { get; set; }
    public string? Comment { get; set; }
}