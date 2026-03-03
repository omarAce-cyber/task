using Microsoft.EntityFrameworkCore;
using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;
using NotificationSystem.DataAccess.Data;

namespace NotificationSystem.DataAccess.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Device>> GetAllAsync()
        => await _context.Devices.ToListAsync();

    public async Task<Device?> GetByTokenAsync(string token)
        => await _context.Devices.FirstOrDefaultAsync(d => d.DeviceToken == token);

    public async Task<Device> AddAsync(Device device)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        return device;
    }
}
