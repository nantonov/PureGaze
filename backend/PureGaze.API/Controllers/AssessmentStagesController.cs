using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;
using PureGaze.Application.UseCases.Evaluation.StartAssessmentStage;

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

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartAssessmentStageCommand startAssessmentStageCommand,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(startAssessmentStageCommand, ct);

        return Ok();
    }
}
