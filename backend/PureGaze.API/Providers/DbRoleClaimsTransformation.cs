using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.API.Providers;

public class DbRoleClaimsTransformation(IEmployeeRepository employeeRepository)
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var email = principal.FindFirstValue(ClaimTypes.Email)
                    ?? principal.FindFirstValue("email");

        if (string.IsNullOrEmpty(email))
            return principal;

        var employee = await employeeRepository.GetByEmailAsync(email);
        if (employee?.ManagerialLevel?.Value is null)
            return principal;

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Role, employee.ManagerialLevel.Value));

        principal.AddIdentity(identity);
        return principal;
    }
}
