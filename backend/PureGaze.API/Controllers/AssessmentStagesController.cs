using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-stages")]
public class AssessmentStagesController(IRequestDispatcher dispatcher)
    : Controller
{
    [HttpPost("assign-me")]
    public async Task<IActionResult> AssignMe([FromBody] AssignAssessmentStageCommand assignAssessmentStageCommand,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(assignAssessmentStageCommand, ct);

        return Ok();
    }
}
