using System.Text.Json.Serialization;

namespace PureGaze.Application.UseCases.Admin.Subtopics.CreateSubtopic;

public sealed class CreateSubtopicResult
{
    [JsonPropertyName("subtopicId")]
    public int SubtopicId { get; init; }
}
