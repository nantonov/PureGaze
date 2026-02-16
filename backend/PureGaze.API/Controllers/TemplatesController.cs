using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Templates.CreateTemplate;
using PureGaze.Application.UseCases.Content.Templates.DeleteTemplate;
using PureGaze.Application.UseCases.Content.Templates.GetTemplates;
using PureGaze.Application.UseCases.Content.Topics.GetTopicsForTemplate;

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

    [HttpGet("{templateId}/topics")]
    public async Task<IActionResult> GetTopicsForTemplate(
        int templateId,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var result = await dispatcher.SendAsync<GetTopicsForTemplateQuery, GetTopicsForTemplateResult>(
            new GetTopicsForTemplateQuery(templateId, page, pageSize), ct);

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
