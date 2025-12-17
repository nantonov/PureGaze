using Microsoft.AspNetCore.Mvc;
using Notification.Application.DTOs;
using Notification.Application.Services;

namespace Notification.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(NotificationService notificationService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto dto, CancellationToken cancellationToken)
    {
        await notificationService.CreateNotificationAsync(dto, cancellationToken);
        return Ok();
    }
}
