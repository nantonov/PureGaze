using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class TopicDto
{
    [JsonPropertyName("id")]
    public int Id  { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name  { get; set; }
}