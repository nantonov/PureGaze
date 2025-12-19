using Common.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.DTOs;
using Notification.Application.Services;

namespace Notification.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController(NotificationService notificationService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto dto, CancellationToken cancellationToken)
    {
        await notificationService.CreateNotificationAsync(dto, cancellationToken);
        return Ok();
    }
    
    [HttpGet("failed")]
    public async Task<IActionResult> GetFailedEmailsAsync(CancellationToken cancellationToken,
        [FromQuery] EmailPriority? priority = null)
    {
        var failedEmails = await notificationService.GetFailedEmailsAsync(priority, cancellationToken);
        return Ok(failedEmails);
    }
}
