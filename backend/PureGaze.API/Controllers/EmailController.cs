using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Notification.GetEmails;
using PureGaze.Application.UseCases.Notification.ResendEmail;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("emails")]
public class EmailController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost]
    public async Task<IActionResult> GetEmails([FromBody] GetEmailsQuery request, CancellationToken ct)
    {
        var result =
            await dispatcher.SendAsync<GetEmailsQuery, GetEmailsResponse>(request, ct);

        return Ok(result);
    }

    [HttpPost("resend")]
    public async Task<IActionResult> ResendEmailManually(Guid id, CancellationToken ct)
    {
        await dispatcher.SendAsync(new ResendEmailCommand(id), ct);

        return Ok();
    }

}
