using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class SubtopicDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("topicId")]
    public int TopicId { get; set; }

    [JsonPropertyName("translates")]
    public List<SubtopicTranslateInfoDto> Translates { get; set; } = [];
}

public class SubtopicTranslateInfoDto
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
