using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

public sealed class UpdateEmployeeLanguageCommand : IRequest
{
    [JsonPropertyName("language")]
    public Language Language { get; init; }
}