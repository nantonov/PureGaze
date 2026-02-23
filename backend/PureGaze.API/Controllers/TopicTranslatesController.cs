using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.TopicTranslates.CreateTopicTranslate;
using PureGaze.Application.UseCases.Admin.TopicTranslates.DeleteTopicTranslate;
using PureGaze.Application.UseCases.Admin.TopicTranslates.EditTopicTranslate;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("topic-translates")]
public sealed class TopicTranslatesController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateTopicTranslate([FromBody] CreateTopicTranslateCommand request,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(request, ct);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditTopicTranslate([FromBody] EditTopicTranslateCommand request, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(request, ct);

        return Ok();
    }

    [HttpDelete("{topicId}/{language}")]
    public async Task<IActionResult> DeleteTopicTranslate([FromRoute] DeleteTopicTranslateCommand request, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(request, ct);

        return Ok();
    }
}
