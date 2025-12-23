using Common.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Contracts.Application;
using Notification.Application.Services;

namespace Notification.Api.Controllers;

[ApiController]
[Route("emails")]
public class EmailController(EmailService emailService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmail([FromBody] CreateEmailRequest dto, CancellationToken cancellationToken)
    {
        await emailService.CreateEmailAsync(dto, cancellationToken);
        return Ok();
    }
    
    [HttpGet("failed")]
    public async Task<IActionResult> GetFailedEmails(CancellationToken cancellationToken)
    {
        var result = 
            await emailService.GetFailedEmailsAsync(cancellationToken);
        
        return Ok(result);
    }
}
