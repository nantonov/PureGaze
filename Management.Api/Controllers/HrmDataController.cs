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
        await employeeService.UploadEmployeesAsync(ct);
        
        return Ok();
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries()
    {
        // var result = await hrmService.GetDictionariesAsync();
        //
        // return Ok(result);
        
        return Ok();
    }
}