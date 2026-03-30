using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Management.UploadDictionaries;

namespace PureGaze.API.Controllers;

[Route("hrmdata")]
[ApiController]
public class HrmDataController(IRequestDispatcher dispatcher) : Controller
{
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionaries(CancellationToken ct)
    {
        await dispatcher.SendAsync(new UploadDictionariesCommand(), ct);

        return Ok();
    }
}