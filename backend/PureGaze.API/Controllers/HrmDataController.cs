using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Management.UploadDictionaries;
using PureGaze.Application.UseCases.Management.UploadEmployee;

namespace PureGaze.API.Controllers;

[Route("hrmdata")]
[ApiController]
public class HrmDataController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployeeById(CancellationToken ct)
    {
        await dispatcher.SendAsync(new UploadEmployeeCommand(), ct);
        
        return Ok();
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries(CancellationToken ct)
    { 
        await dispatcher.SendAsync(new UploadDictionariesCommand(), ct);
        
        return Ok();
    }
}