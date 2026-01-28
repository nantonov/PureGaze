using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

public sealed class UpdateEmployeeThemeCommand : IRequest
{
    [JsonIgnore]
    public string Email { get; set; }
    
    [JsonPropertyName("theme")]
    public Theme Theme { get; init; }
}