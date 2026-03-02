using Microsoft.EntityFrameworkCore;
using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;
using NotificationSystem.DataAccess.Data;

namespace NotificationSystem.DataAccess.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
        => await _context.Notifications.OrderByDescending(n => n.CreatedAt).ToListAsync();

    public async Task<Notification?> GetByIdAsync(int id)
        => await _context.Notifications.FindAsync(id);

    public async Task<Notification> AddAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }
}
