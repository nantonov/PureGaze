using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class TemplateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("codeName")]
    public string? CodeName { get; set; }
}
