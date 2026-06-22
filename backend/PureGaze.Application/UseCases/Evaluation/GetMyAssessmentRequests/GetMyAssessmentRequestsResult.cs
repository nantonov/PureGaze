using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public sealed record GetMyAssessmentRequestsResult(int Total, IReadOnlyList<GetMyAssessmentRequestDto> AssessmentRequests);

public sealed class GetMyAssessmentRequestDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")]
    public string? EmployeeFullName { get; set; }

    [JsonPropertyName("managerFullName")]
    public string? ManagerFullName { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("status")]
    public AssessmentRequestStatus Status { get; set; }

    public static GetMyAssessmentRequestDto ToDto(AssessmentRequest request)
        => new()
        {
            Id = request.Id,
            EmployeeFullName = $"{request.Employee?.FirstNameEn} {request.Employee?.LastNameEn}".Trim(),
            ManagerFullName = $"{request.Manager?.FirstNameEn} {request.Manager?.LastNameEn}".Trim(),
            Code = request.Code?.Name,
            Status = request.Status,
        };
}
