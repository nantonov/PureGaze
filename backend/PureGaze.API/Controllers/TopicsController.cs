using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Topics.CreateTopic;
using PureGaze.Application.UseCases.Admin.Topics.DeleteTopic;
using PureGaze.Application.UseCases.Admin.TopicTranslates.GetTopicTranslates;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("topics")]
public class TopicsController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateTopic([FromBody] CreateTopicCommand request,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<CreateTopicCommand, CreateTopicResult>(request, ct);

        return Ok(result);
    }

    [HttpDelete("{topicId}")]
    public async Task<IActionResult> DeleteTopic(int topicId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteTopicCommand(topicId), ct);

        return Ok();
    }

    [HttpGet("{topicId}/translates")]
    public async Task<IActionResult> GetTopicTranslates(
        [FromRoute] int topicId,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetTopicTranslatesQuery, GetTopicTranslatesResult>(
            new GetTopicTranslatesQuery(topicId, page, pageSize), ct);

        return Ok(result);
    }
}
