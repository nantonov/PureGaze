using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.CreateSubtopicScore;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("subtopic-scores")]
public class SubtopicScoresController(IRequestDispatcher dispatcher)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateSubtopicScore([FromBody] CreateSubtopicScoreCommand createSubtopicScoreRequest,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(createSubtopicScoreRequest, ct);

        return Ok();
    }
}
