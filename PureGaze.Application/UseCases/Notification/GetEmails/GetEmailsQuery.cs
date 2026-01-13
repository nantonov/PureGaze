using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public class GetEmailsQuery : IRequest<GetEmailsResponse>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    
    [JsonPropertyName("status")]
    public EmailStatus Status { get; set; }
}