using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Templates.CreateTemplate;
using PureGaze.Application.UseCases.Admin.Templates.DeleteTemplate;
using PureGaze.Application.UseCases.Admin.Templates.GetTemplates;
using PureGaze.Application.UseCases.Admin.Topics.GetTopicsForTemplate;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("templates")]
public class TemplatesController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTemplates([FromQuery] GetTemplatesQuery request,
        CancellationToken ct = default)
    {
        GetTemplatesQueryResult response = await dispatcher.SendAsync<GetTemplatesQuery, GetTemplatesQueryResult>(request, ct);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand request,
        CancellationToken ct = default)
    {
        CreateTemplateResult response = await dispatcher.SendAsync<CreateTemplateCommand, CreateTemplateResult>(request, ct);

        return Ok(response);
    }

    [HttpGet("{templateId}/topics")]
    public async Task<IActionResult> GetTopicsForTemplate(
        [FromRoute] int templateId,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken ct = default)
    {
        GetTopicsForTemplateResult response = await dispatcher.SendAsync<GetTopicsForTemplateQuery, GetTopicsForTemplateResult>(
            new GetTopicsForTemplateQuery(templateId, page, pageSize), ct);

        return Ok(response);
    }

    [HttpDelete("{templateId}")]
    public async Task<IActionResult> DeleteTemplate(int templateId,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new DeleteTemplateCommand(templateId), ct);

        return Ok();
    }
}
