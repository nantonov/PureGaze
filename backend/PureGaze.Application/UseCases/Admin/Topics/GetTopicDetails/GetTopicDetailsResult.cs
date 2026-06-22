using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicDetails;

public sealed class GetTopicDetailsResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("translates")]
    public IReadOnlyList<GetTopicDetailsTranslateResult> Translates { get; set; } = [];

    public static GetTopicDetailsResult ToResult(Topic topic)
        => new()
        {
            Id = topic.Id,
            Translates = [..topic.TopicTranslates.Select(t => new GetTopicDetailsTranslateResult
            {
                Language = t.Language,
                Name = t.Name ?? ""
            })]
        };

}

public sealed class GetTopicDetailsTranslateResult
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
