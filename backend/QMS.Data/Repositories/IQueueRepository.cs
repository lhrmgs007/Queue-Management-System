using QMS.Core.Entities;

namespace QMS.Data.Repositories;

public interface IQueueRepository
{
    Task<IEnumerable<Queue>> GetAllQueuesAsync();
    Task<Queue?> GetQueueByIdAsync(int id);
    Task<Queue?> GetQueueByNameAsync(string name);
    Task<Queue> AddQueueAsync(Queue queue);
    Task<Queue> UpdateQueueAsync(Queue queue);
    Task DeleteQueueAsync(int id);
    Task<int> GetNextTicketNumberAsync(int queueId);
}
