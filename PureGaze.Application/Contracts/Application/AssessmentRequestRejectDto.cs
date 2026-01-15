using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentRequestRejectDto
{
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}