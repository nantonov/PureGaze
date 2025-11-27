using Management.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers;

[Route("hrm")]
[ApiController]
public class HrmController(IHrmService hrmService)
    : ControllerBase
{
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployeeById()
    {
        var result = await hrmService.GetEmployeesAsync();

        return Ok(result);
    }
}