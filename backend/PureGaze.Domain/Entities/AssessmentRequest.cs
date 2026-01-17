using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class AssessmentRequest : BaseEntity<int>
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public int ManagerId { get; set; }
    public Employee Manager { get; set; } = null!;

    public int CodeId { get; set; }
    public Code Code { get; set; } = null!;
    
    public AssessmentRequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}