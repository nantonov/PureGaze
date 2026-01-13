using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class AssessmentStage : BaseEntity<Guid>
{
    public Assessment Assessment { get; set; } = null!;

    public string StageType { get; set; } = null!;
    public Guid? AssessorId { get; set; }
    
    public string? Summary { get; set; }
    public bool? IsRecommended { get; set; }
    
    public StageStatus Status { get; set; }
    public ICollection<QuestionScore> Scores { get; set; } = new List<QuestionScore>();
}