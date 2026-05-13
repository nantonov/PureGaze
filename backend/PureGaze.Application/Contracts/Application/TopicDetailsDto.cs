using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class TopicDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("translates")]
    public List<TopicTranslateInfoDto> Translates { get; set; } = [];
}

public class TopicTranslateInfoDto
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
