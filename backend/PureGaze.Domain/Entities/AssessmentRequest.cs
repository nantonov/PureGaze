using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class AssessmentRequest : BaseEntity<int>
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public int CodeId { get; set; }
    public Code? Code { get; set; }

    public AssessmentRequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}