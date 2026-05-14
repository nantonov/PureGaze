using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.AssignAssessmentStage;
using PureGaze.Application.UseCases.Evaluation.CompleteAssessmentStage;
using PureGaze.Application.UseCases.Evaluation.StartAssessmentStage;
using PureGaze.Application.UseCases.Evaluation.UnassignAssessmentStage;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-stages")]
public class AssessmentStagesController(IRequestDispatcher dispatcher) : Controller
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

    [HttpPost("unassign-me")]
    public async Task<IActionResult> UnassignMe([FromBody] UnassignAssessmentStageCommand command,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Complete([FromBody] CompleteAssessmentStageCommand completeAssessmentStageCommand,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(completeAssessmentStageCommand, ct);

        return Ok();
    }
}
