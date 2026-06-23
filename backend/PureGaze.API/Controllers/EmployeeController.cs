using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Staff.GetCurrentEmployee;
using PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

namespace PureGaze.API.Controllers;

[Route("employees")]
[ApiController]
[Authorize]
public class EmployeeController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        GetCurrentEmployeeResult response =
            await dispatcher
                .SendAsync<GetCurrentEmployeeQuery, GetCurrentEmployeeResult>(new GetCurrentEmployeeQuery(), ct);

        return Ok(response);
    }

    [HttpPut("language")]
    public async Task<IActionResult> UpdateLanguage([FromBody] UpdateEmployeeLanguageCommand command, CancellationToken ct)
    {
        await dispatcher.SendAsync(command, ct);

        return Ok();
    }
}