using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class SubtopicScore : BaseEntity<int>
{
    public int StageId { get; set; }
    public AssessmentStage? Stage { get; set; }

    public int SubtopicId { get; set; }
    public Subtopic? Subtopic { get; set; }

    public AssessmentMark Score { get; set; }
    public string? Comment { get; set; }
}