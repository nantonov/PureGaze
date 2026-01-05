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
    public async Task<IActionResult> CreateEmail([FromBody] CreateEmailRequest request, CancellationToken ct)
    {
        await emailService.CreateEmailAsync(request, ct);
        return Ok();
    }
    
    [HttpPost("resend")]
    public async Task<IActionResult> ResendEmailManually(Guid id, CancellationToken ct)
    {
        await emailService.ResendEmailManuallyAsync(id, ct);
        return Ok();
    }

}
