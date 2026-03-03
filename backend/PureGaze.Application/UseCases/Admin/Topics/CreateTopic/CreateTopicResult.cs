using System.Text.Json.Serialization;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed class CreateTopicResult
{
    [JsonPropertyName("topicId")]
    public int TopicId { get; init; }
}