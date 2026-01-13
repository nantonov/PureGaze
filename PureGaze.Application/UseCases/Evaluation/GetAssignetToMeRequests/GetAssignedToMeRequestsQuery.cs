using System.Text.Json.Serialization;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignetToMeRequests;

public class GetAssignedToMeRequestsQuery : IRequest<GetAssignedToMeRequestsResult>
{
    [JsonIgnore]
    public int ManagerId { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }   
}