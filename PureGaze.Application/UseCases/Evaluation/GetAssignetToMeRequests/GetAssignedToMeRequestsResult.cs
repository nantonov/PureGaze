using System.Text.Json.Serialization;
using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignetToMeRequests;

public class GetAssignedToMeRequestsResult
{
    [JsonPropertyName("assessmentRequests")]
    public IReadOnlyList<AssessmentRequestDto> AssessmentRequests { get; set; } = [];
}