using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentStageItemDto
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("topicName")] 
    public string TopicName { get; set; } = string.Empty;

    [JsonPropertyName("assessorFullName")] 
    public string? AssessorFullName { get; set; }

    [JsonPropertyName("isAssignedToCurrentUser")] 
    public bool IsAssignedToCurrentUser { get; set; }
}
