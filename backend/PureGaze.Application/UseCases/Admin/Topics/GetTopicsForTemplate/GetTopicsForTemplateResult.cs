using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicsForTemplate;

public sealed record GetTopicsForTemplateResult(IReadOnlyList<GetTopicsForTemplateDto> Topics) : IRequest;

public sealed class GetTopicsForTemplateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public static GetTopicsForTemplateDto ToDto(Topic topic)
        => new()
        {
            Id = topic.Id,
            Name = topic.TopicTranslates.FirstOrDefault()?.Name
        };
}
