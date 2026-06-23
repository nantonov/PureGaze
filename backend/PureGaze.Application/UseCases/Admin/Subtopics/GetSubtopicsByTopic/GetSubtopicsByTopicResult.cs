using System.Text.Json.Serialization;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicsByTopic;

public sealed class GetSubtopicsByTopicResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
