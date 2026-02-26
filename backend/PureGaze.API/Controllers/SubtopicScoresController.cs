using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.ScoreSubtopic;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("subtopic-scores")]
public class SubtopicScoresController(IRequestDispatcher dispatcher)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateSubtopicScore([FromBody] ScoreSubtopicCommand scoreSubtopicRequest,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(scoreSubtopicRequest, ct);

        return Ok();
    }
}
