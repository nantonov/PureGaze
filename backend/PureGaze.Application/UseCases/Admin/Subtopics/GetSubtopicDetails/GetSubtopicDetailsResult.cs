using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicDetails;

public sealed class GetSubtopicDetailsResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("topicId")]
    public int TopicId { get; set; }

    [JsonPropertyName("translates")]
    public IReadOnlyList<GetSubtopicDetailsTranslateResult> Translates { get; set; } = [];

    public static GetSubtopicDetailsResult ToResult(Subtopic subtopic)
        => new()
        {
            Id = subtopic.Id,
            TopicId = subtopic.TopicId,
            Translates = [.. subtopic.SubtopicTranslates.Select(t => new GetSubtopicDetailsTranslateResult
            {
                Language = t.Language,
                Name = t.Name ?? ""
            })]
        };
}

public sealed class GetSubtopicDetailsTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
