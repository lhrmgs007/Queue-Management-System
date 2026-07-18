using QMS.Core.Entities;
using QMS.Data.Repositories;

namespace QMS.Services.Queues;

public interface IQueueService
{
    Task<IEnumerable<Queue>> GetAllQueuesAsync();
    Task<Queue?> GetQueueByIdAsync(int id);
    Task<Queue> CreateQueueAsync(string queueName, string prefix, string color = "#3b82f6");
    Task<Queue> UpdateQueueAsync(Queue queue);
    Task DeleteQueueAsync(int id);
    Task<string> GenerateTicketNumberAsync(int queueId);
}

public class QueueService : IQueueService
{
    private readonly IQueueRepository _queueRepository;

    public QueueService(IQueueRepository queueRepository)
    {
        _queueRepository = queueRepository;
    }

    public async Task<IEnumerable<Queue>> GetAllQueuesAsync()
    {
        return await _queueRepository.GetAllQueuesAsync();
    }

    public async Task<Queue?> GetQueueByIdAsync(int id)
    {
        return await _queueRepository.GetQueueByIdAsync(id);
    }

    public async Task<Queue> CreateQueueAsync(string queueName, string prefix, string color = "#3b82f6")
    {
        var queue = new Queue
        {
            QueueName = queueName,
            Prefix = prefix,
            Color = color,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CurrentTicketNumber = 0,
            NextTicketNumber = 1
        };

        return await _queueRepository.AddQueueAsync(queue);
    }

    public async Task<Queue> UpdateQueueAsync(Queue queue)
    {
        return await _queueRepository.UpdateQueueAsync(queue);
    }

    public async Task DeleteQueueAsync(int id)
    {
        await _queueRepository.DeleteQueueAsync(id);
    }

    public async Task<string> GenerateTicketNumberAsync(int queueId)
    {
        var queue = await _queueRepository.GetQueueByIdAsync(queueId);
        if (queue == null)
            throw new InvalidOperationException($"Queue {queueId} not found");

        var nextNumber = await _queueRepository.GetNextTicketNumberAsync(queueId);
        return $"{queue.Prefix}{nextNumber:0000}";
    }
}
