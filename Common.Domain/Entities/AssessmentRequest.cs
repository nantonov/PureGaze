using Common.Data.Enums;

namespace Common.Domain.Entities;

public class AssessmentRequest : BaseEntity<int>
{
    public int CandidateId { get; set; }
    public virtual Employee Candidate { get; set; } = null!;

    public int AssignedM1Id { get; set; }
    public virtual Employee AssignedM1 { get; set; } = null!;
    
    public Code Code { get; set; } = null!;
    public DateTime RequestedToDate { get; set; }
    public AssessmentRequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}