using System.Text.Json.Serialization;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public class GetMyAssessmentRequestsQuery : IRequest<GetMyAssessmentRequestsResult>
{
    [JsonIgnore]
    public int EmployeeId { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }   
}