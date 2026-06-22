using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;

public sealed class GetQuestionsBySubtopicResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("subTopicId")]
    public int SubTopicId { get; set; }

    [JsonPropertyName("translates")]
    public List<GetQuestionsBySubtopicTranslateResult> Translates { get; set; } = [];

    public static GetQuestionsBySubtopicResult ToResult(Question question)
        => new()
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            Translates = [.. question.QuestionTranslates.Select(t => new GetQuestionsBySubtopicTranslateResult
            {
                Language = t.Language,
                Content = t.Content ?? ""
            })]
        };
}

public sealed class GetQuestionsBySubtopicTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
