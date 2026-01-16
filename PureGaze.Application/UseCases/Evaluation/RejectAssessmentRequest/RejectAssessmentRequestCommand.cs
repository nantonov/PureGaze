using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
namespace PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

public class RejectAssessmentRequestCommand : IRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}