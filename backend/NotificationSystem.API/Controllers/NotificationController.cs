using Microsoft.AspNetCore.Mvc;
using NotificationSystem.API.DTOs;
using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;

namespace NotificationSystem.API.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendNotificationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Body))
            return BadRequest(new { message = "Title and Body are required." });

        var result = await _notificationService.SendNotificationAsync(
            request.Title, request.Body, request.SentBy);

        return Ok(new ApiResponse<Notification>
        {
            Message = result.IsSent
                ? "Notification sent successfully."
                : "Notification was saved but could not be delivered. Please check your Firebase configuration.",
            Data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notifications = await _notificationService.GetAllNotificationsAsync();
        return Ok(notifications);
    }
}
