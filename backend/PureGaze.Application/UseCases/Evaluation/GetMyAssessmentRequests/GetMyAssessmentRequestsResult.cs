using System.Text.Json.Serialization;
using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public class GetMyAssessmentRequestsResult
{
    [JsonPropertyName("assessmentRequests")]
    public IReadOnlyList<AssessmentRequestDto> AssessmentRequests { get; set; } = [];
}