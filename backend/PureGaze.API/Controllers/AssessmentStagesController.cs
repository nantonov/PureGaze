using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-stages")]
public class AssessmentStagesController(
    IRequestDispatcher dispatcher, 
    ICurrentUserContextProvider currentUserContextProvider)
    : BaseController
{
    [HttpPost("assign-me")]
    public async Task<IActionResult> AssignMe([FromBody] int assessmentStageId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new AssignAssessmentStageCommand(assessmentStageId, currentUserContextProvider.GetUserEmail()), ct);

        return Ok();
    }
}
