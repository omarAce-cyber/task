namespace NotificationSystem.Core.Entities;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string SentBy { get; set; } = string.Empty;
    public bool IsSent { get; set; }

    public ICollection<NotificationLog> Logs { get; set; } = new List<NotificationLog>();
}
