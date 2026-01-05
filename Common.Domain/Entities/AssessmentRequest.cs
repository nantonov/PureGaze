using Common.Data.Enums;

namespace Common.Domain.Entities;

public class AssessmentRequest : BaseEntity<int>
{
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } = null!;

    public int ManagerId { get; set; }
    public virtual Employee Manager { get; set; } = null!;

    public int CodeId { get; set; }
    public virtual Code Code { get; set; } = null!;
    
    public AssessmentRequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}