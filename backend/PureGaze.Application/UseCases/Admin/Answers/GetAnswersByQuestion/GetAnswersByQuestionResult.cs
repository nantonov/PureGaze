using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswersByQuestion;

public sealed class GetAnswersByQuestionResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("translates")]
    public IReadOnlyList<GetAnswersByQuestionTranslateResult> Translates { get; set; } = [];

    public static GetAnswersByQuestionResult ToResult(Answer answer)
        => new()
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Translates = [.. answer.AnswerTranslates.Select(t => new GetAnswersByQuestionTranslateResult
            {
                Language = t.Language,
                Content = t.Content ?? ""
            })]
        };
}

public sealed class GetAnswersByQuestionTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
