using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

public sealed class UpdateEmployeeLanguageCommand : IRequest
{
    [JsonIgnore]
    public string Email { get; set; }
    
    [JsonPropertyName("language")]
    public Language Language { get; init; }
}