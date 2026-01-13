using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class EmailDto
{
    [JsonPropertyName("from")]
    public string? From { get; set; }
        
    [JsonPropertyName("to")]
    public string? To { get; set; }
        
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }
        
    [JsonPropertyName("status")]
    public EmailStatus Status { get; set; }
}