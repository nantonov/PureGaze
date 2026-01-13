using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class QuestionScore : BaseEntity<int>
{
    public int StageId { get; set; }
    public AssessmentStage Stage { get; set; } = null!;
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    
    public AssessmentMark Score { get; set; }
    public string? Comment { get; set; }
}