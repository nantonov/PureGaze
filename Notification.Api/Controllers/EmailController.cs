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
        CancellationToken cancellationToken = default)
    {
        var result = await emailService.GetEmailsAsync(page, pageSize, status, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmail([FromBody] CreateEmailRequest dto, CancellationToken cancellationToken)
    {
        await emailService.CreateEmailAsync(dto, cancellationToken);
        return Ok();
    }
    
    [HttpPost("resend-failed")]
    public async Task<IActionResult> ResendFailedEmails(CancellationToken cancellationToken)
    {
        await emailService.ResendFailedEmailsAsync(cancellationToken);
        return Ok();
    }

}
