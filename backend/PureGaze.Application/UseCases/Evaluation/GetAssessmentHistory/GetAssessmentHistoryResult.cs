using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

public sealed record GetAssessmentHistoryResult(int Total, IReadOnlyList<GetAssessmentHistoryDto> Items);

public sealed class GetAssessmentHistoryDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")]
    public string EmployeeFullName { get; set; } = string.Empty;

    [JsonPropertyName("employeeEmail")]
    public string EmployeeEmail { get; set; } = string.Empty;

    [JsonPropertyName("gradeRange")]
    public string GradeRange { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    public static GetAssessmentHistoryDto ToDto(Assessment assessment)
        => new()
        {
            Id = assessment.Id,
            EmployeeFullName = $"{assessment.Employee?.FirstNameEn} {assessment.Employee?.LastNameEn}".Trim(),
            EmployeeEmail = assessment.Employee?.Email ?? string.Empty,
            GradeRange = $"{assessment.Code?.Grade?.Translation} -> {assessment.Code?.ToGrade?.Translation}",
            Status = assessment.Status.ToString(),
            CreatedAt = assessment.CreatedAt
        };
}
