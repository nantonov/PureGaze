using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Management.UploadDictionaries;
using PureGaze.Application.UseCases.Management.UploadEmployee;

namespace PureGaze.API.Controllers;

[Route("hrmdata")]
[ApiController]
public class HrmDataController(IRequestDispatcher dispatcher)
    : ControllerBase
{
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployeeById(CancellationToken ct)
    {
        await dispatcher.SendAsync<UploadEmployeeCommand>(new UploadEmployeeCommand(), ct);
        
        return Ok();
    }
    
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries(CancellationToken ct)
    { 
        await dispatcher.SendAsync<UploadDictionariesCommand>(new UploadDictionariesCommand(), ct);
        
        return Ok();
    }
}