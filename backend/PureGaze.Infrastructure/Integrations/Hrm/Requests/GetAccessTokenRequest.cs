using System.Text.Json.Serialization;

namespace PureGaze.Infrastructure.Integrations.Hrm.Requests;

public class GetAccessTokenRequest
{
    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClinetId { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}