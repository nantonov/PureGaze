using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;

public sealed class GetQuestionDetailsResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("subTopicId")]
    public int SubTopicId { get; set; }

    [JsonPropertyName("translates")]
    public List<GetQuestionDetailsTranslateResult> Translates { get; set; } = [];

    [JsonPropertyName("answer")]
    public GetQuestionDetailsAnswerResult? Answer { get; set; }

    public static GetQuestionDetailsResult ToDto(Question question)
        => new()
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            Translates = [.. question.QuestionTranslates.Select(t => new GetQuestionDetailsTranslateResult
            {
                Language = t.Language,
                Content = t.Content ?? ""
            })],
            Answer = question.Answer is null ? null : new GetQuestionDetailsAnswerResult
            {
                Id = question.Answer.Id,
                QuestionId = question.Answer.QuestionId,
                Translates = [.. question.Answer.AnswerTranslates.Select(t => new GetQuestionDetailsAnswerTranslateResult
                {
                    Language = t.Language,
                    Content = t.Content ?? ""
                })]
            }
        };
}

public sealed class GetQuestionDetailsTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}

public sealed class GetQuestionDetailsAnswerResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("translates")]
    public List<GetQuestionDetailsAnswerTranslateResult> Translates { get; set; } = [];
}

public sealed class GetQuestionDetailsAnswerTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
