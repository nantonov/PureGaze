using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Subtopics.CreateSubtopic;
using PureGaze.Application.UseCases.Admin.Subtopics.DeleteSubtopic;
using PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicDetails;
using PureGaze.Application.UseCases.Admin.Subtopics.UpdateSubtopic;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("subtopics")]
public class SubtopicController(IRequestDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubtopicCommand command, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<CreateSubtopicCommand, CreateSubtopicResult>(command, ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetSubtopicDetailsQuery, SubtopicDetailsDto>(new GetSubtopicDetailsQuery(id), ct);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubtopicCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteSubtopicCommand(id), ct);
        return Ok();
    }
}
