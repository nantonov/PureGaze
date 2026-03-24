using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Codes.CreateCode;
using PureGaze.Application.UseCases.Admin.Codes.DeleteCode;
using PureGaze.Application.UseCases.Admin.Codes.EditCode;
using PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;
using PureGaze.Application.UseCases.Admin.Codes.GetCodes;
using PureGaze.Application.UseCases.Admin.ProfessionalLevels.GetProfessionalLevels;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("codes")]
[Authorize(Roles = "M3,M4,M5")]
public class AdminController(IRequestDispatcher dispatcher) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCodes(CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetAllCodesQuery, List<CodeDto>>(new GetAllCodesQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCodes([FromRoute] int id, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetCodesQuery, CodeDto>(new GetCodesQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCode([FromBody] CreateCodeCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditCode([FromBody] EditCodeCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCode([FromRoute] int id, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteCodeCommand(id), ct);
        return Ok();
    }

    [HttpGet("/professional-levels")]
    public async Task<IActionResult> GetProfessionalLevels(CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetProfessionalLevelsQuery, List<ProfessionalLevelDto>>(
            new GetProfessionalLevelsQuery(), ct);
        return Ok(result);
    }
}
