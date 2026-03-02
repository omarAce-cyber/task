namespace NotificationSystem.Core.Entities;

public class Device
{
    public int Id { get; set; }
    public string DeviceToken { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }

    public ICollection<NotificationLog> Logs { get; set; } = new List<NotificationLog>();
}
