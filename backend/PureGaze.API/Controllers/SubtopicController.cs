using Microsoft.AspNetCore.Mvc;
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
        CreateSubtopicResult result = await dispatcher.SendAsync<CreateSubtopicCommand, CreateSubtopicResult>(command, ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct = default)
    {
        GetSubtopicDetailsResult response =
            await dispatcher.SendAsync<GetSubtopicDetailsQuery, GetSubtopicDetailsResult>(
                new GetSubtopicDetailsQuery(id), ct);
        return Ok(response);
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
