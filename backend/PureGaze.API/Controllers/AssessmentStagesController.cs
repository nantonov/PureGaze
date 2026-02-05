using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-stages")]
public class AssessmentStagesController(IRequestDispatcher dispatcher)
    : BaseController
{
    [HttpPost("assign-me")]
    public async Task<IActionResult> AssignMe([FromBody] int AssessmentStageId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new AssingAssesmentStageCommand(AssessmentStageId, Email), ct);

        return Ok();
    }
}
