namespace NotificationSystem.Core.Interfaces;

public interface IFcmService
{
    Task<bool> SendToAllDevicesAsync(string title, string body);
    Task<bool> SendToDeviceAsync(string deviceToken, string title, string body);
}
