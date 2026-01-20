using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class AnswerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("translates")]
    public List<AnswerTranslateInfoDto> Translates { get; set; } = [];
}
