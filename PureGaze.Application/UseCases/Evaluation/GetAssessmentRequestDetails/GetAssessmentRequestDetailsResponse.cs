using System.Text.Json.Serialization;
using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;

public class GetAssessmentRequestDetailsResponse
{
    [JsonPropertyName("details")]
    public AssessmentRequestDetailsDto Details { get; set; }
}