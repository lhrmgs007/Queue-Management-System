using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;
using QMS.Data.Context;

namespace QMS.Data.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly QmsDbContext _context;

    public ServiceRepository(QmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetServicesByQueueAsync(int queueId)
    {
        return await _context.Services
            .Where(s => s.QueueId == queueId && s.IsActive)
            .ToListAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int id)
    {
        return await _context.Services
            .FirstOrDefaultAsync(s => s.ServiceId == id);
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<Service> UpdateServiceAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task DeleteServiceAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            service.IsActive = false;
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
