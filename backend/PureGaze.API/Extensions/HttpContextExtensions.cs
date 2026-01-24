using System.IdentityModel.Tokens.Jwt;

namespace PureGaze.API.Extensions;

public static class HttpContextExtensions
{
    public static string? GetEmail(this HttpRequest request)
    {
        var authHeader = request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authHeader))
            return string.Empty;
        
        var token = authHeader["Bearer ".Length..];
            
        var handler = new JwtSecurityTokenHandler();

        return handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == "email")?.Value;
    }
}