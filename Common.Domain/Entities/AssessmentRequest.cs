using Common.Data.Enums;

namespace Common.Domain.Entities;

public class AssessmentRequest : BaseEntity<int>
{
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } = null!;

    public int? M1Id { get; set; }
    public virtual Employee? M1 { get; set; }

    public int? M3Id { get; set; }
    public virtual Employee? M3 { get; set; }

    public int CodeId { get; set; }
    public virtual Code Code { get; set; } = null!;
    public DateTime RequestedToDate { get; set; }
    public AssessmentRequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}