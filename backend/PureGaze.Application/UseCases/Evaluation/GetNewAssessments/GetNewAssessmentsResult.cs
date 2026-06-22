using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetNewAssessments;

public sealed record GetNewAssessmentsResult(IReadOnlyList<GetNewAssessmentDto> Items);

public sealed class GetNewAssessmentDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")]
    public string EmployeeFullName { get; set; } = string.Empty;

    [JsonPropertyName("employeeEmail")]
    public string EmployeeEmail { get; set; } = string.Empty;

    [JsonPropertyName("gradeRange")]
    public string GradeRange { get; set; } = string.Empty;

    [JsonPropertyName("isOwnAssessment")]
    public bool IsOwnAssessment { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("stages")]
    public List<GetNewAssessmentsStageDto> Stages { get; set; } = [];

    public static GetNewAssessmentDto ToDto(Assessment assessment, int currentUserId)
        => new()
        {
            Id = assessment.Id,
            EmployeeFullName = $"{assessment.Employee?.FirstNameEn} {assessment.Employee?.LastNameEn}".Trim(),
            EmployeeEmail = assessment.Employee?.Email ?? string.Empty,
            GradeRange = $"{assessment.Code?.Grade?.Translation} -> {assessment.Code?.ToGrade?.Translation}",
            IsOwnAssessment = assessment.EmployeeId == currentUserId,
            Status = assessment.Status.ToString(),
            Stages = assessment.Stages
                .Select(stage => GetNewAssessmentsStageDto.ToDto(stage, currentUserId))
                .ToList()
        };
}

public sealed class GetNewAssessmentsStageDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("topicName")]
    public string TopicName { get; set; } = string.Empty;

    [JsonPropertyName("assessorFullName")]
    public string? AssessorFullName { get; set; }

    [JsonPropertyName("isAssignedToCurrentUser")]
    public bool IsAssignedToCurrentUser { get; set; }

    public static GetNewAssessmentsStageDto ToDto(AssessmentStage stage, int currentUserId)
        => new()
        {
            Id = stage.Id,
            TopicName = stage.Topic?.TopicTranslates.OrderBy(translate => translate.Language).FirstOrDefault()?.Name ?? string.Empty,
            AssessorFullName = stage.Assessor is null ? null : $"{stage.Assessor.FirstNameEn} {stage.Assessor.LastNameEn}".Trim(),
            IsAssignedToCurrentUser = stage.AssessorId == currentUserId
        };
}
