using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Templates.CreateTemplate;
using PureGaze.Application.UseCases.Content.Templates.DeleteTemplate;
using PureGaze.Application.UseCases.Content.Templates.GetTemplates;
using PureGaze.Application.UseCases.Content.Topics.GetTopicForTemplate;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("templates")]
public class TemplatesController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand request,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<CreateTemplateCommand, CreateTemplateResult>(request, ct);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> QueryTemplates([FromQuery] GetTemplatesQuery request,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetTemplatesQuery, GetTemplatesQueryResult>(request, ct);

        return Ok(result);
    }

    [HttpGet("{templateId}/topic")]
    public async Task<IActionResult> GetTopicForTemplate(int templateId,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetTopicForTemplateQuery, GetTopicForTemplateResult>(
            new GetTopicForTemplateQuery(templateId), ct);

        return Ok(result);
    }

    [HttpDelete("{templateId}")]
    public async Task<IActionResult> DeleteTemplate(int templateId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteTemplateCommand(templateId), ct);

        return Ok();
    }
}
