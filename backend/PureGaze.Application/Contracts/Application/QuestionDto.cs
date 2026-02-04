using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class QuestionDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("subTopicId")]
    public int SubTopicId { get; set; }

    [JsonPropertyName("translates")]
    public List<QuestionTranslateInfoDto> Translates { get; set; } = [];
}
