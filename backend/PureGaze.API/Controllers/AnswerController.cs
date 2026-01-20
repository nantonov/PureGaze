using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Answers.GetAnswerDetails;
using PureGaze.Application.UseCases.Content.Answers.UpdateAnswer;
using PureGaze.Application.UseCases.Content.Answers.GetAnswersByQuestion;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("answers")]
public class AnswerController(IRequestDispatcher dispatcher) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetAnswerDetailsQuery, AnswerDetailsDto>(new GetAnswerDetailsQuery(id), ct);
        return Ok(result);
    }

    [HttpGet("by-question/{questionId}")]
    public async Task<IActionResult> GetByQuestion([FromRoute] int questionId, CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetAnswersByQuestionQuery, AnswerDto?>(new GetAnswersByQuestionQuery(questionId), ct);
        
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAnswerCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        return Ok();
    }
}
