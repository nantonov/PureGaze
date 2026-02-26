using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.CreateDirectAssessment;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessments")]
public class AssessmentController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost("create-direct")]
    public async Task<IActionResult> CreateDirect([FromBody] CreateDirectAssessmentCommand command,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);

        return Ok();
    }
}
