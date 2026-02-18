using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;
using PureGaze.Application.UseCases.Admin.Questions.UpdateQuestion;
using PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;
using PureGaze.Application.UseCases.Admin.Questions.CreateQuestionWithAnswer;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("questions")]
public class QuestionController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct = default)
    {
        var result =
            await dispatcher.SendAsync<GetQuestionDetailsQuery, QuestionDetailsDto>(new GetQuestionDetailsQuery(id), ct);
        
        return Ok(result);
    }

    [HttpGet("subtopic/{subTopicId}")]
    public async Task<IActionResult> GetBySubtopic([FromRoute] int subTopicId, CancellationToken ct = default)
    {
        var result = 
            await dispatcher.SendAsync<GetQuestionsBySubtopicQuery, List<QuestionDto>>(new GetQuestionsBySubtopicQuery(subTopicId), ct);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionWithAnswerCommand command, CancellationToken ct = default)
    {
        var result = 
            await dispatcher.SendAsync<CreateQuestionWithAnswerCommand, int>(command, ct);
        
        return CreatedAtAction(nameof(Get), new { id = result }, new { Id = result });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuestionCommand command, CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);
        
        return Ok();
    }
}
