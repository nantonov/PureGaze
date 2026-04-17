using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicsByTopic;
using PureGaze.Application.UseCases.Admin.Topics.CreateTopic;
using PureGaze.Application.UseCases.Admin.Topics.DeleteTopic;
using PureGaze.Application.UseCases.Admin.Topics.EditTopic;

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

    [HttpPut]
    public async Task<IActionResult> EditTopic([FromBody] EditTopicCommand editTopicCommand,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(editTopicCommand, ct);

        return Ok();
    }

    [HttpDelete("{topicId}")]
    public async Task<IActionResult> DeleteTopic([FromRoute] int topicId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteTopicCommand(topicId), ct);

        return Ok();
    }

    [HttpGet("{topicId}/subtopics")]
    public async Task<IActionResult> GetSubtopics([FromRoute] int topicId,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetSubtopicsByTopicQuery, List<SubtopicListItemDto>>(
            new GetSubtopicsByTopicQuery(topicId), ct);

        return Ok(result);
    }
}
