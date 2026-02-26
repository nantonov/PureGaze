using System.Text.Json.Serialization;

namespace PureGaze.Infrastructure.Integrations.Hrm.Responses;

public class GetAccessTokenResponse
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }
}