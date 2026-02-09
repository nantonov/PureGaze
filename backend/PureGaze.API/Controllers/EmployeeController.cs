using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Staff.GetCurrentEmployee;
using PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;
using PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

namespace PureGaze.API.Controllers;

[Route("employees")]
[ApiController]
[Authorize]
public class EmployeeController(IRequestDispatcher dispatcher) 
    : BaseController
{
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        GetCurrentEmployeeResponse response = 
            await dispatcher
                .SendAsync<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>(new GetCurrentEmployeeQuery(), ct);
        
        return Ok(response);
    }
    
    [HttpPut("theme")]
    public async Task<IActionResult> UpdateTheme([FromBody]UpdateEmployeeThemeCommand command, CancellationToken ct)
    {
        await dispatcher.SendAsync(command, ct);
        
        return Ok();
    }
    
    [HttpPut("language")]
    public async Task<IActionResult> UpdateLanguage([FromBody]UpdateEmployeeLanguageCommand command, CancellationToken ct)
    {
        await dispatcher.SendAsync(command, ct);
        
        return Ok();
    }
}