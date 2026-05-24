using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.CompleteAssessment;
using PureGaze.Application.UseCases.Evaluation.CreateDirectAssessment;
using PureGaze.Application.UseCases.Evaluation.GetNewAssessments;
using PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

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

    [HttpGet("new")]
    public async Task<IActionResult> GetNew(CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetNewAssessmentsQuery, GetNewAssessmentsResult>(new GetNewAssessmentsQuery(), ct);
        return Ok(result);
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Complete([FromBody] CompleteAssessmentCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }

    [HttpPost("history")]
    public async Task<IActionResult> GetHistory([FromBody] GetAssessmentHistoryQuery query, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetAssessmentHistoryQuery, GetAssessmentHistoryResult>(query, ct);
        return Ok(result);
    }
}
