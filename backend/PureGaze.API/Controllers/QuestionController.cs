using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Questions.CreateQuestionWithAnswer;
using PureGaze.Application.UseCases.Admin.Questions.DeleteQuestion;
using PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;
using PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;
using PureGaze.Application.UseCases.Admin.Questions.UpdateQuestion;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("questions")]
public class QuestionController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct = default)
    {
        GetQuestionDetailsResult response =
            await dispatcher.SendAsync<GetQuestionDetailsQuery, GetQuestionDetailsResult>(new GetQuestionDetailsQuery(id), ct);

        return Ok(response);
    }

    [HttpGet("subtopic/{subTopicId}")]
    public async Task<IActionResult> GetBySubtopic([FromRoute] int subTopicId, CancellationToken ct = default)
    {
        IReadOnlyList<GetQuestionsBySubtopicResult> result =
            await dispatcher.SendAsync<GetQuestionsBySubtopicQuery, List<GetQuestionsBySubtopicResult>>(
                new GetQuestionsBySubtopicQuery(subTopicId), ct);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionWithAnswerCommand command, CancellationToken ct = default)
    {
        int result =
            await dispatcher.SendAsync<CreateQuestionWithAnswerCommand, int>(command, ct);

        return CreatedAtAction(nameof(Get), new { id = result }, new { Id = result });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuestionCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteQuestionCommand(id), ct);

        return Ok();
    }
}
