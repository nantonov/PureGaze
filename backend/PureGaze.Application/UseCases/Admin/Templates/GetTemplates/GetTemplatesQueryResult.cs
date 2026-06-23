using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Templates.GetTemplates;

public sealed record GetTemplatesQueryResult(List<GetTemplateDto> Templates) : IRequest;

public sealed class GetTemplateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("codeName")]
    public string? CodeName { get; set; }

    [JsonPropertyName("codeDisplay")]
    public string? CodeDisplay { get; set; }

    public static GetTemplateDto ToDto(Template template)
        => new()
        {
            Id = template.Id,
            CodeName = template.Code?.Name,
            CodeDisplay = $"{template.Code?.Grade?.Translation} -> {template.Code?.ToGrade?.Translation}"
        };
}
