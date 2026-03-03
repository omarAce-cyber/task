using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;
using NotificationSystem.DataAccess.Data;

namespace NotificationSystem.DataAccess.Repositories;

public class NotificationLogRepository : INotificationLogRepository
{
    private readonly AppDbContext _context;

    public NotificationLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationLog> AddAsync(NotificationLog log)
    {
        _context.NotificationLogs.Add(log);
        await _context.SaveChangesAsync();
        return log;
    }
}
