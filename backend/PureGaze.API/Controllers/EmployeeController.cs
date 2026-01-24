using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

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
                .SendAsync<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>(new GetCurrentEmployeeQuery(Email), ct);
        
        return Ok(response);
    }
}