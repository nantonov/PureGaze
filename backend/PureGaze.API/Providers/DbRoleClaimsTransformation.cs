using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.API.Providers;

public class DbRoleClaimsTransformation(IEmployeeRepository employeeRepository)
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Email)
                    ?? principal.FindFirstValue("email");

        if (string.IsNullOrEmpty(email))
            return principal;

        Employee? employee = await employeeRepository.GetByEmailAsync(email);
        if (employee?.ManagerialLevel?.Value is null)
            return principal;

        ClaimsIdentity identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Role, employee.ManagerialLevel.Value));

        principal.AddIdentity(identity);
        return principal;
    }
}
