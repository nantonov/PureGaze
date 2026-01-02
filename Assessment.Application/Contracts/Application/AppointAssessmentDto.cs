namespace Assessment.Application.Contracts.Application;

public record AppointAssessmentDto
{
	public int EmployeeId { get; init; }
	public int M1Id { get; init; }
	public int? M3Id { get; init; }
	public int CodeId { get; init; }
	public DateTime RequestedToDate { get; init; }
}