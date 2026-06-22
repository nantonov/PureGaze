using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswerDetails;

public sealed class GetAnswerDetailsResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("translates")]
    public IReadOnlyList<GetAnswerDetailsTranslateResult> Translates { get; set; } = [];

    public static GetAnswerDetailsResult ToResult(Answer answer)
        => new()
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Translates = [.. answer.AnswerTranslates.Select(t => new GetAnswerDetailsTranslateResult
            {
                Language = t.Language,
                Content = t.Content ?? ""
            })]
        };
}

public sealed class GetAnswerDetailsTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
