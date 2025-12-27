using Management.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers;

[Route("hrmdata")]
[ApiController]
public class HrmDataController(IEmployeeService employeeService)
    : ControllerBase
{
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployeeById(CancellationToken ct)
    {
        var result = await employeeService.UploadEmployeesAsync(ct);
        
        return Ok(result);
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries()
    {
        return Ok();
    }
}