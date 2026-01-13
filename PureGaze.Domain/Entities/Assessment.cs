using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class Assessment : BaseEntity<Guid>
{
    public Guid TemplateId { get; set; }
    public Guid CandidateId { get; set; }
    public AssessmentStatus Status { get; set; }
    public string TargetGrade { get; set; } = string.Empty;
    public ICollection<AssessmentStage> Stages { get; set; } = new List<AssessmentStage>();
}