using Microsoft.AspNetCore.Mvc;
using NotificationSystem.API.DTOs;
using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;

namespace NotificationSystem.API.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDeviceRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DeviceToken))
            return BadRequest(new { message = "DeviceToken is required." });

        var device = await _deviceService.RegisterDeviceAsync(
            request.DeviceToken, request.DeviceName);

        return Ok(new ApiResponse<Device>
        {
            Message = "Device registered successfully.",
            Data = device
        });
    }
}
