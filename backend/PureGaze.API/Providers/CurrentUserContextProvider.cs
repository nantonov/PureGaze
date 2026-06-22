using PureGaze.Application.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace PureGaze.API.Providers;

internal sealed class CurrentUserContextProvider(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserContextProvider
{
    public string GetUserEmail()
    {
        HttpRequest? request = httpContextAccessor.HttpContext?.Request;

        string? authHeader = request?.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authHeader))
            return string.Empty;

        string token = authHeader["Bearer ".Length..];

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        return handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? string.Empty;
    }
}
