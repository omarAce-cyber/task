using Microsoft.EntityFrameworkCore;
using NotificationSystem.Core.Entities;

namespace NotificationSystem.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<NotificationLog> NotificationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationLog>()
            .HasOne(l => l.Notification)
            .WithMany(n => n.Logs)
            .HasForeignKey(l => l.NotificationId);

        modelBuilder.Entity<NotificationLog>()
            .HasOne(l => l.Device)
            .WithMany(d => d.Logs)
            .HasForeignKey(l => l.DeviceId);
    }
}
