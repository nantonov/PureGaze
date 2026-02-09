using PureGaze.Application.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace PureGaze.API.Providers;

internal sealed class CurrentUserContextProvider(IHttpContextAccessor httpContextAccessor) 
    : ICurrentUserContextProvider
{
    public string GetUserEmail()
    {
        var request = httpContextAccessor.HttpContext?.Request;

        var authHeader = request?.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authHeader))
            return string.Empty;

        var token = authHeader["Bearer ".Length..];

        var handler = new JwtSecurityTokenHandler();

        return handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? string.Empty;
    }
}
