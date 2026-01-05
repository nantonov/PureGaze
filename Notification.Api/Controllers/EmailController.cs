using Common.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Abstractions.Services;
using Notification.Application.Contracts.Application;

namespace Notification.Api.Controllers;

[ApiController]
[Route("emails")]
public class EmailController(IEmailService emailService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEmails(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20, 
        [FromQuery] EmailStatus status = EmailStatus.ExceededRetryCount, 
        CancellationToken ct = default)
    {
        var result = await emailService.GetEmailsAsync(page, pageSize, status, ct);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmail([FromBody] CreateEmailRequest dto, CancellationToken ct)
    {
        await emailService.CreateEmailAsync(dto, ct);
        return Ok();
    }
    
    [HttpPost("resend-failed")]
    public async Task<IActionResult> ResendFailedEmails(CancellationToken ct)
    {
        await emailService.ResendFailedEmailsAsync(ct);
        return Ok();
    }

}
