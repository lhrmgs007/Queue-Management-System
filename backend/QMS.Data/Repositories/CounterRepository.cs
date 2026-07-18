using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;
using QMS.Data.Context;

namespace QMS.Data.Repositories;

public class CounterRepository : ICounterRepository
{
    private readonly QmsDbContext _context;

    public CounterRepository(QmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Counter>> GetAllCountersAsync()
    {
        return await _context.Counters
            .Include(c => c.Service)
            .Include(c => c.Users)
            .Where(c => c.IsActive)
            .ToListAsync();
    }

    public async Task<Counter?> GetCounterByIdAsync(int id)
    {
        return await _context.Counters
            .Include(c => c.Service)
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.CounterId == id);
    }

    public async Task<Counter> AddCounterAsync(Counter counter)
    {
        _context.Counters.Add(counter);
        await _context.SaveChangesAsync();
        return counter;
    }

    public async Task<Counter> UpdateCounterAsync(Counter counter)
    {
        _context.Counters.Update(counter);
        await _context.SaveChangesAsync();
        return counter;
    }

    public async Task DeleteCounterAsync(int id)
    {
        var counter = await _context.Counters.FindAsync(id);
        if (counter != null)
        {
            counter.IsActive = false;
            _context.Counters.Update(counter);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Counter>> GetCountersByServiceAsync(int serviceId)
    {
        return await _context.Counters
            .Include(c => c.Service)
            .Where(c => c.ServiceId == serviceId && c.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Counter>> GetOpenCountersAsync()
    {
        return await _context.Counters
            .Include(c => c.Service)
            .Where(c => c.Status == "Open" && c.IsActive)
            .ToListAsync();
    }
}
