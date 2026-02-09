using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class SubtopicScore : BaseEntity<int>
{
    public required int StageId { get; set; }
    public AssessmentStage? Stage { get; set; }
    
    public required int SubtopicId { get; set; }
    public Subtopic? Subtopic { get; set; }
    
    public required AssessmentMark Score { get; set; }
    public string? Comment { get; set; }
}