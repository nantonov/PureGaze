using Management.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers;

[Route("hrmdata")]
[ApiController]
public class HrmDataController(IHrmService hrmService)
    : ControllerBase
{
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployeeById(CancellationToken ct)
    {
        await hrmService.UploadEmployeesAsync(ct);
        
        return Ok();
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries(CancellationToken ct)
    { 
        await hrmService.UploadDictionariesAsync(ct);
        
        return Ok();
    }
}