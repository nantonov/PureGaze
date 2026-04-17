using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class SubtopicListItemDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
