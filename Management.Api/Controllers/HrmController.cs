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
        var result = hrmService.GetEmployeesAsync();

        return Ok(result);
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries()
    {
        var result = await hrmService.GetDictionariesAsync();

        return Ok(result);
    }
}