using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;

public sealed record GetAssessmentRequestDetailsResult(GetAssessmentRequestDetailDto Details);

public sealed class GetAssessmentRequestDetailDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }

    [JsonPropertyName("employeeFullName")]
    public string? EmployeeFullName { get; set; }

    [JsonPropertyName("managerId")]
    public int ManagerId { get; set; }

    [JsonPropertyName("managerFullName")]
    public string? ManagerFullName { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("status")]
    public AssessmentRequestStatus Status { get; set; }

    [JsonPropertyName("rejectionReason")]
    public string? RejectionReason { get; set; }

    public static GetAssessmentRequestDetailDto ToDto(AssessmentRequest request)
        => new()
        {
            Id = request.Id,
            EmployeeId = request.EmployeeId,
            EmployeeFullName = $"{request.Employee?.FirstNameEn} {request.Employee?.LastNameEn}".Trim(),
            ManagerId = request.ManagerId,
            ManagerFullName = $"{request.Manager?.FirstNameEn} {request.Manager?.LastNameEn}".Trim(),
            Code = request.Code?.Name,
            Status = request.Status,
            RejectionReason = request.RejectionReason
        };
}
