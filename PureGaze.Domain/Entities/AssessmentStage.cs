using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class AssessmentStage : BaseEntity<int>
{
    public Assessment Assessment { get; set; } = null!;

    public string StageType { get; set; } = null!;
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
    public int? AssessorId { get; set; }
    public Employee? Assessor { get; set; }
    
    public string? Summary { get; set; }
    public bool? IsRecommended { get; set; }
    
    public StageStatus Status { get; set; }
    public ICollection<QuestionScore> Scores { get; set; } = new List<QuestionScore>();
}