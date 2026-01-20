using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class QuestionDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("subTopicId")]
    public int SubTopicId { get; set; }

    [JsonPropertyName("translates")]
    public List<QuestionTranslateInfoDto> Translates { get; set; } = [];

    [JsonPropertyName("answer")]
    public AnswerDetailsDto? Answer { get; set; }
}

public class QuestionTranslateInfoDto
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}

public class AnswerDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("translates")]
    public List<AnswerTranslateInfoDto> Translates { get; set; } = [];
}

public class AnswerTranslateInfoDto
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
