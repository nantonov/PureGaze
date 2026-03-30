using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Employees.GetEmployees;
using PureGaze.Application.UseCases.Admin.Employees.UploadEmployee;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("admin-employees")]
[Authorize(Roles = "M3,M4,M5")]
public class AdminEmployeeController(IRequestDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GetEmployees([FromBody] GetEmployeesQuery query, CancellationToken ct)
    {
        var result =
            await dispatcher.SendAsync<GetEmployeesQuery, GetEmployeesResult>(query, ct);

        return Ok(result);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> GetEmployeeById(CancellationToken ct)
    {
        await dispatcher.SendAsync(new UploadEmployeeCommand(), ct);

        return Ok();
    }
}