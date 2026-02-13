using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Templates.CreateTemplate;
using PureGaze.Application.UseCases.Content.Templates.DeleteTemplate;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("templates")]
public class TemplatesController(IRequestDispatcher dispatcher)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand request,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(request, ct);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTemplate([FromBody] DeleteTemplateCommand request,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(request, ct);

        return Ok();
    }
}
