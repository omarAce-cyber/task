namespace NotificationSystem.API.DTOs;

public class RegisterDeviceRequest
{
    public string DeviceToken { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
}
