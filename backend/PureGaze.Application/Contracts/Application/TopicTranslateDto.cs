using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class TopicTranslateDto
{
    [JsonPropertyName("topicId")]
    public int TopicId { get; set; }

    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}