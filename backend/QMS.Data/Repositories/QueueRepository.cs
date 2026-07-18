using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;
using QMS.Data.Context;

namespace QMS.Data.Repositories;

public class QueueRepository : IQueueRepository
{
    private readonly QmsDbContext _context;

    public QueueRepository(QmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Queue>> GetAllQueuesAsync()
    {
        return await _context.Queues
            .Include(q => q.Services)
            .Where(q => q.IsActive)
            .ToListAsync();
    }

    public async Task<Queue?> GetQueueByIdAsync(int id)
    {
        return await _context.Queues
            .Include(q => q.Services)
            .FirstOrDefaultAsync(q => q.QueueId == id);
    }

    public async Task<Queue?> GetQueueByNameAsync(string name)
    {
        return await _context.Queues
            .FirstOrDefaultAsync(q => q.QueueName == name && q.IsActive);
    }

    public async Task<Queue> AddQueueAsync(Queue queue)
    {
        _context.Queues.Add(queue);
        await _context.SaveChangesAsync();
        return queue;
    }

    public async Task<Queue> UpdateQueueAsync(Queue queue)
    {
        _context.Queues.Update(queue);
        await _context.SaveChangesAsync();
        return queue;
    }

    public async Task DeleteQueueAsync(int id)
    {
        var queue = await _context.Queues.FindAsync(id);
        if (queue != null)
        {
            queue.IsActive = false;
            _context.Queues.Update(queue);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetNextTicketNumberAsync(int queueId)
    {
        var queue = await _context.Queues.FindAsync(queueId);
        if (queue == null)
            throw new InvalidOperationException($"Queue {queueId} not found");

        queue.NextTicketNumber++;
        await _context.SaveChangesAsync();
        return queue.NextTicketNumber;
    }
}
